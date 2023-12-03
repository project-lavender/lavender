using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    //左クリックからインタラクトできる時間
    [SerializeField] float iTime = 0.2f;
    [SerializeField] UIcontroller uictr;
    [SerializeField] ItemStack itemStack;
    [SerializeField] DT_Item dtitem;
    [SerializeField] DemoPlayer demoPlayer;
    [SerializeField] bool touchingOnother = false;
    

    // Start is called before the first frame update
    [SerializeField]
    Gimicks gimicks = null;
    RaycastHit hit;
    void Start()
    {
        //uictr = GetComponent<UIcontroller>();
        //itemStack = GetComponent<ItemStack>();
        demoPlayer = FindAnyObjectByType<DemoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.8f);
        if (Physics.Raycast(transform.position, transform.forward * 0.8f, out hit))
        {
            //とりあえず触れている
            if (hit.collider.CompareTag("Gimick"))
            {
                if (gimicks == null)
                {
                    Gimicks tmpgm = hit.collider.GetComponent<Gimicks>();
                    gimicks = tmpgm;
                    gimicks.EmitColor();
                }
            }
        }
        //ギミックから離れたらオフ
        if (gimicks != null && hit.collider.gameObject != gimicks.gameObject)
        {
            gimicks.TurnOffColor();
            gimicks = null;
        }
        //インタラクトボタンオン
        if (gimicks!=null && Input.GetMouseButtonDown(0))
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

                //アイテム追加
                Debug.Log("itemID->" + dT.itemID);
                //GameObject i = dtitem.FindItem(dT.itemID);
                //itemStack.AddItem(i);
                itemStack.EnableItem(dT.itemID);
                itemStack.DisableItem(dT.downFrag);
                //gimicks.TurnOffColor();
                //gimicks = null;

                //イベント発動
                Debug.Log(dT.demoID);
                demoPlayer.DemoPlay(dT.demoID);
            }
        }
    }
}
