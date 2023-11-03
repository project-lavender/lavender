using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Item : ScriptableObject
{
    public List<DTItem> ItemsList = new List<DTItem>();

    //itemのコンポーネントを返す
    public Items FindItem(string id)
    {
        Items items = null;
        foreach(DTItem i in ItemsList)
        {
            if (id == i.id)
            {
                items = i.item.GetComponent<Items>();
            }
        }

        return items;
    }
}
