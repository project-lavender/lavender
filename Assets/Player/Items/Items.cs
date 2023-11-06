using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //UIに表示するアイテム名
    public string itemname = "";
    public string useTextID = "";
    public Sprite itemIcon;
    public virtual string UseItem()
    {
        Debug.Log("Default Item Use " + gameObject.name);
        return useTextID;
    }
}
