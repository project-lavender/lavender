using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTableHolder : ScriptableObject
{
    public List<ItemStructure> ItemsList = new List<ItemStructure>();

    //itemのコンポーネントを返す
    public GameObject FindItem(string id)
    {
        GameObject item = null;
        foreach(ItemStructure i in ItemsList)
        {
            if (id == i.id)
            {
                item = Instantiate(i.item);
            }
        }

        return item;
    }
}
