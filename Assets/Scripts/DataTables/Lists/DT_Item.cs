using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Item : ScriptableObject
{
    public List<DTItem> ItemsList = new List<DTItem>();

    //item�̃R���|�[�l���g��Ԃ�
    public GameObject FindItem(string id)
    {
        GameObject item = null;
        foreach(DTItem i in ItemsList)
        {
            if (id == i.id)
            {
                item = Instantiate(i.item);
            }
        }

        return item;
    }
}
