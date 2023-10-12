using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class RierMover : MonoBehaviour
{
    [SerializeField]
    private TextAsset param;
    private CharacterController ctr;
    [SerializeField]
    private float PL_cam_height = 2.2f;
    [SerializeField]
    private float PL_light_intencity = 10f;
    [SerializeField]
    private float PL_walk = 3.0f;
    [SerializeField]
    private float PL_run = 6.0f;

    [SerializeField]
    private float leapTime = 0.4f;
    //歩行とダッシュのカメラ揺れ
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    [SerializeField] Vector2[] noiseSettings;
    [SerializeField] int noisemode,nownoise;
    
    private float g = 9.8f;
    private float h, v, c;
    float lt = 0f;
    Cinemachine.CinemachineBasicMultiChannelPerlin nz;

    [SerializeField]
    private List<string[]> p = new();
    Light l;
    [SerializeField] Animator anm,anmmesh;

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

    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
        anm = GetComponent<Animator>();
        nz = vc.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        //anmmesh = GetComponentInChildren<Animator>();
        l = GetComponentInChildren<Light>();
        Debug.Log(param.text);
        string ptext = param.text;
        string[] enterspl = ptext.Split("\n", System.StringSplitOptions.None);
        foreach (string s in enterspl){
            p.Add(s.Split(",", System.StringSplitOptions.None));
        }

        Debug.Log(p[1][1]);
        Debug.Log(p[2][1]);
        Debug.Log(p[3][1]);
        Debug.Log(p[4][1]);
        //PL_cam_height = float.Parse( p[1][1]);
        //PL_light_intencity = float.Parse( p[2][1]);
        h = 0f;
        v = 0f;
        //ctr.Move((Camera.main.transform.forward * v + Camera.main.transform.right * h + Vector3.down * g) * c * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        ctr.height = PL_cam_height;
        l.intensity = PL_light_intencity;

        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (nownoise != noisemode && lt<leapTime)
        {
            NoiseSet(noisemode);
        }
        if ((h * h + v * v) <  0.1f)
        {
            noisemode = 0;
            
            anm.SetBool("walk", false);
            anm.SetBool("run", false);
            anmmesh.SetBool("walk", false);
            anmmesh.SetBool("run", false);
        }
        else
        {
            noisemode = 1;
            anm.SetBool("walk", true);
            anmmesh.SetBool("walk", true);
            anmmesh.SetFloat("x", h);
            anmmesh.SetFloat("y", v);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anm.SetBool("run", true);
                anmmesh.SetBool("run", true);
                c = PL_run;
                noisemode = 2;
            }
            else
            {
                anm.SetBool("run", false);
                anmmesh.SetBool("run", false);
                c = PL_walk;
                
            }
        }
        
        ctr.Move((Camera.main.transform.forward * v +Camera.main.transform.right * h + Vector3.down * g) * c * Time.deltaTime);
        Vector3 lookVec = Camera.main.transform.rotation.eulerAngles;
        lookVec = new Vector3(0f, lookVec.y, 0f);
        anmmesh.transform.rotation = Quaternion.Euler(lookVec);
    }

}
