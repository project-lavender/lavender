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
    public bool interactTrigger = false;

    

    // Start is called before the first frame update
    [SerializeField]
    Gimicks gimicks = null;


    IEnumerator InteractTrigger()
    {
        interactTrigger = true;
        yield return new WaitForSeconds(iTime);
        interactTrigger = false;
    }
    void Start()
    {
        //uictr = GetComponent<UIcontroller>();
        //itemStack = GetComponent<ItemStack>();
        demoPlayer = FindAnyObjectByType<DemoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(InteractTrigger());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Gimick")
        {
            Gimicks tmpgm = other.GetComponent<Gimicks>();
            if (gimicks == null)
            {
                gimicks = tmpgm;
                gimicks.EmitColor();
            }else if(gimicks != null)
            {
                gimicks.TurnOffColor();
                gimicks = tmpgm;
                gimicks.EmitColor();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Gimick" && interactTrigger)
        {
            interactTrigger = false;
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
                Debug.Log(dT.itemID);
                GameObject i = dtitem.FindItem(dT.itemID);
                itemStack.AddItem(i);

                //gimicks.TurnOffColor();
                //gimicks = null;

                //イベント発動
                Debug.Log(dT.demoID);
                demoPlayer.DemoPlay(dT.demoID);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gimicks != null && other.gameObject == gimicks.gameObject)
        {
            gimicks.TurnOffColor();
            gimicks = null;
        }
    }
}
