using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField] UIcontroller uictr;
    [SerializeField] ItemStack itemStack;
    [SerializeField] DT_Item dtitem;
    [SerializeField] DemoPlayer demoPlayer;
    [SerializeField] bool touchingOnother = false;
    [SerializeField] Gimicks gimicks = null;
    [SerializeField] UIcontroller uic; 
    private Lavender action;


    // Start is called before the first frame update
    RaycastHit hit;
    void Start()
    {
        //コールバック設定
        action = new Lavender();
        action.Enable();
        demoPlayer = FindAnyObjectByType<DemoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new();
        ray.origin = transform.position;
        ray.direction = transform.forward;
        Debug.DrawRay(transform.position, transform.forward * 0.8f);

        if (uic.nowID == -1 && Physics.Raycast(ray, out hit, 0.8f) && hit.collider.CompareTag("Gimick"))
        {
            if (gimicks != null)
            {
                gimicks.TurnOffColor();
                gimicks = null;
            }
            Gimicks tmpgm = hit.collider.GetComponent<Gimicks>();
            gimicks = tmpgm;
            gimicks.EmitColor();

        }
        else if (gimicks != null)
        {
            //ギミックから離れたらオフ
            gimicks.TurnOffColor();
            gimicks = null;
        }
        
        //インタラクトボタンオン
        if (gimicks!=null && action.Player.Fire.triggered)
        {
            //StartCoroutine(InteractTrigger());
            string textid;
            DTGimick dT;
            //ui 起動
            if (gimicks != null)
            {
                dT = gimicks.InteractGimick();
                if (dT == null)
                {
                    return;
                }
                textid = dT.textID;
                Debug.Log(textid);
                uictr.ActiveUI(textid);

                itemStack.EnableItem(dT.itemID);
                itemStack.DisableItem(dT.downFrag);

                //イベント発動
                Debug.Log(dT.demoID);
                demoPlayer.DemoPlay(dT.demoID);
            }
        }
    }
}
