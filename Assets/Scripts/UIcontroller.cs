using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] RectTransform[] UIs;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;

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
        vc.gameObject.SetActive(b);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
