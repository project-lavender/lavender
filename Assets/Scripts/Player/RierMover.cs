using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.InputSystem;
public class RierMover : MonoBehaviour
{
    [SerializeField]
    private TextAsset param;
    private Rigidbody rb;
    [SerializeField]
    private float PL_walk = 3.0f;
    [SerializeField]
    private float PL_run = 6.0f;
    //しゃがみ
    [SerializeField]
    private float crouchCamHeight = -0.7f;
    [SerializeField]
    private float leapTime = 0.4f;

    //しゃがみと起ち状態の高さの影響度0~1
    public float highorCrouch = 1.0f;

    //歩行とダッシュのカメラ揺れ
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    [SerializeField] Vector2[] noiseSettings;
    [SerializeField] int noisemode,nownoise;
    
    private float h, v, c;
    float lt = 0f;
    Cinemachine.CinemachineBasicMultiChannelPerlin nz;
    [SerializeField] Transform mcam;

    [SerializeField]
    private List<string[]> p = new();
    [SerializeField] Animator anm, anmmesh;

    //inputSYstem
    [SerializeField] private Lavender action;


    void NoiseSet(int i)
    {

        float ampNow = noiseSettings[nownoise].x;
        float freqNow = noiseSettings[nownoise].y;
        float ampNext = noiseSettings[i].x;
        float freqNext = noiseSettings[i].y;
        float amp, freq;
        
        lt += Time.deltaTime;
        amp = Mathf.Lerp(ampNow, ampNext, lt / leapTime);
        freq = Mathf.Lerp(freqNow, freqNext, lt / leapTime);
        nz.m_AmplitudeGain = amp;
        nz.m_FrequencyGain = freq;
        if (lt / leapTime > 1f)
        {
            nownoise = i;
            lt = 0f;
        }

    }

    void Move(InputAction.CallbackContext callback)
    {
        //直立状態でないと歩かない
        
        if (!anm.GetBool("crouch") && callback.started)
        {
            noisemode = 1;
            c = PL_walk;
            anm.SetBool("walk", true);
            anmmesh.SetBool("walk", true);
        }
        else if (callback.performed)
        {
            Vector2 inputVal = action.Player.Move.ReadValue<Vector2>();
            h = inputVal.x;
            v = inputVal.y;
            anmmesh.SetFloat("x", h);
            anmmesh.SetFloat("y", v);
        }
        else if (callback.canceled)
        {
            noisemode = 0;
            c = 0f;
            h = 0f;
            v = 0f;
            anm.SetBool("walk", false);
            anm.SetBool("run", false);
            anmmesh.SetBool("walk", false);
            anmmesh.SetBool("run", false);
        }
    }
    void Crouch(InputAction.CallbackContext callback)
    {

        //しゃがみ状態なら
        if (callback.started)
        {
            noisemode = 0;
            c = 0f;
            anm.SetBool("run", false);
            anmmesh.SetBool("run", false);
            anm.SetBool("walk", false);
            anmmesh.SetBool("walk", false);
            anm.SetBool("crouch", true);
            anmmesh.SetBool("crouch", true);
        }
        else if (callback.canceled)
        {

            anm.SetBool("crouch", false);
            anmmesh.SetBool("crouch", false);
            if (h * h + v * v > 0f)
            {
                noisemode = 1;
                c = PL_walk;
                anm.SetBool("walk", true);
                anmmesh.SetBool("walk", true);
            }
        }
    }
    void Run(InputAction.CallbackContext callback)
    {
        if (callback.canceled)
        {
            //しゃがみから離しても歩かない
            if (anm.GetBool("crouch"))
            {
                noisemode = 0;
                c = 0;
            }
            else
            {
                noisemode = 1;
                c = PL_walk;
            }
            
            anm.SetBool("run", false);
            anmmesh.SetBool("run", false);
        }
        else if ( noisemode == 1 && callback.started)
        {
            noisemode = 2;
            anm.SetBool("run", true);
            anmmesh.SetBool("run", true);
            c = PL_run;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //ctr = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //mcam = Camera.main.transform;
        anm = GetComponent<Animator>();
        //コールバック設定
        action = new Lavender();
        action.Player.Move.started += Move;
        action.Player.Move.performed +=Move;
        action.Player.Move.canceled += Move;
        action.Player.Crouch.started += Crouch;
        action.Player.Crouch.canceled += Crouch;
        action.Player.Run.started += Run;
        action.Player.Run.canceled += Run;
        action.Enable();

        nz = vc.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        string ptext = param.text;
        string[] enterspl = ptext.Split("\n", System.StringSplitOptions.None);
        foreach (string s in enterspl){
            p.Add(s.Split(",", System.StringSplitOptions.None));
        }
        c = PL_walk;
        h = 0f;
        v = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {

        Vector3 camh = Vector3.up * Mathf.Lerp(0f, crouchCamHeight, highorCrouch);
        vc.transform.localPosition = camh;
        if (nownoise != noisemode && lt < leapTime)
        {
            NoiseSet(noisemode);
        }
        Vector3 lookVec = Camera.main.transform.rotation.eulerAngles;
        lookVec = new Vector3(0f, lookVec.y, 0f);
        anmmesh.transform.rotation = Quaternion.Euler(lookVec);
    }
    private void FixedUpdate()
    {
        Vector3 vvec = Vector3.Scale(mcam.forward, Vector3.one - Vector3.up);
        Vector3 hvec = Vector3.Scale(mcam.right, Vector3.one - Vector3.up);
        rb.AddForce((vvec * v + hvec * h).normalized * c, ForceMode.Force);
    }

}
