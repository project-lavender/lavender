using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] RectTransform[] UIs;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    Cinemachine.CinemachinePOV pov;
    
    Vector2 sence;

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
    }
    public void SetVirtualCamera(bool b)
    {
        if (b) {
            pov.m_HorizontalAxis.m_MaxSpeed = sence.x;
            pov.m_VerticalAxis.m_MaxSpeed = sence.y;
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
        pov = vc.GetCinemachineComponent<Cinemachine.CinemachinePOV>();
        sence = Vector2.right * pov.m_HorizontalAxis.m_MaxSpeed + Vector2.up * pov.m_VerticalAxis.m_MaxSpeed;
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
    }
}
