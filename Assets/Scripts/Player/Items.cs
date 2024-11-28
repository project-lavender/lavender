using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //UIに表示するアイテム名
    public string itemname = "";
    public string useTextID = "";
    public string myfrag;
    public Sprite itemIcon;

    
    [SerializeField] DT_Frag frags;

    public void SetFrag(bool f)
    {
        if (myfrag != "")
        {
            frags.SetVal(myfrag, f);
        }
    }
    public virtual string UseItem()
    {
        Debug.Log("Default Item Use " + gameObject.name);
        //frags.SetVal(myfrag, true);
        return useTextID;
    }
}
