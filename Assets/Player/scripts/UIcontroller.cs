using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class UIcontroller : MonoBehaviour
{
    [SerializeField] TextAsset playesence;
    [SerializeField] string pathsence = "Assets/Player/scripts/DataTable/PlayerSence.json";
    [SerializeField] Vector2 maxSence;
    [SerializeField] RectTransform[] UIs;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    Cinemachine.CinemachinePOV pov;
    [SerializeField] Slider xslider, yslder;

    [SerializeField]
    private class Sence
    {
        public float Xsence;
        public float Ysence;
    }

    Sence sence;
    Vector2 nowvec;

    public void ActiveUI(int i)
    {
        int j = 0;
        foreach(RectTransform r in UIs)
        {
            r.gameObject.SetActive(false);
            if (i == j)
            {
                
                r.gameObject.SetActive(true);
            }
            j += 1;
        }
        if (i < 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void XSence()
    {
        nowvec.x = xslider.value * maxSence.x;
    }
    public void YSence()
    {
        nowvec.y = yslder.value * maxSence.y;
    }
    public void WriteSence()
    {
        pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
        pov.m_VerticalAxis.m_MaxSpeed = nowvec.y;
        sence.Xsence = xslider.value;
        sence.Ysence = yslder.value;
        string json = JsonUtility.ToJson(sence);
        StreamWriter wr = new StreamWriter(pathsence);
        wr.WriteLine(json);
        wr.Close();
    }

    public void SetVirtualCamera(bool b)
    {
        if (b) {
            pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
            pov.m_VerticalAxis.m_MaxSpeed = nowvec.x;
        }
        else
        {
            pov.m_HorizontalAxis.m_MaxSpeed = 0f;
            pov.m_VerticalAxis.m_MaxSpeed = 0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pov = vc.GetCinemachineComponent<Cinemachine.CinemachinePOV>();
        sence = JsonUtility.FromJson<Sence>(playesence.text);
        Debug.Log(sence.Xsence);
        //maxSence = Vector2.right * pov.m_HorizontalAxis.m_MaxSpeed + Vector2.up * pov.m_VerticalAxis.m_MaxSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        //Hanyo
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveUI(0);
            SetVirtualCamera(false);
        }
        //Text
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveUI(1);
            SetVirtualCamera(false);
        }
        //Voice
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveUI(2);
            
        }
        //Esc
        if (Input.GetKeyDown(KeyCode.Escape)){
            ActiveUI(3);
            SetVirtualCamera(false);
            //sence = JsonUtility.FromJson<Sence>(playesence.text);
            xslider.value = sence.Xsence;
            yslder.value = sence.Ysence;
        }
    }
}
