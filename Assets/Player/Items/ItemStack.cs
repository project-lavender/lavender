using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemStack : MonoBehaviour
{
    [SerializeField] float itemRingLength = 5f;
    [SerializeField] Transform itemIconAnker;
    [SerializeField] GameObject itemIconPrefab;
    [SerializeField] UIcontroller uic;
    [SerializeField] List<Items> items;
    [SerializeField] List<Image> itemIcons;
    [SerializeField] TMP_Text itemname;
    [SerializeField] Color selectColor, Offcolor;
    
    [SerializeField] int rotI = 0,nowitem = 0;

    int itemN = 0;
    void LineupItems()
    {
        float angle;
        if (itemN != 0)
        {
            angle = 360f / itemN;
        }
        else
        {
            angle = 360f;
        }
        for (int i = 0; i < itemN; i++)
        {
            float rad = Mathf.Deg2Rad * angle * i;
            itemIcons[i].transform.localPosition = new Vector3(itemRingLength * Mathf.Sin(rad), itemRingLength * Mathf.Cos(rad), 0f);
            itemIcons[i].color = Offcolor;
        }
        itemIcons[nowitem].color = selectColor;
        itemname.text = items[nowitem].itemname;
    }

    public void AddItem(GameObject item)
    {
        if (item == null)
        {
            return;
        }
        Items I = item.GetComponent<Items>();
        items.Add(I);
        GameObject iconobj = Instantiate(itemIconPrefab, itemIconAnker); 
        //iconobj.transform.SetParent(itemIconAnker);
        Image iconImg = iconobj.GetComponent<Image>();
        itemIcons.Add(iconImg);
        iconImg.sprite = I.itemIcon;
        itemN += 1;
        nowitem = 0;
        LineupItems();
    }

    void RotateItem(int r)
    {
        float angle = 0f;
        if (itemN != 0)
        {
            angle = 360f / itemN;
        }
        else
        {
            r = 0;
        }
        rotI += r;
        nowitem = Mathf.Abs(rotI) % itemN;

        if (itemN < 2)
        {
            nowitem = 0;
        }
        else if (rotI > 0)
        {
            nowitem = itemN - nowitem;
            if (nowitem == itemN)
            {
                nowitem = 0;
            }
        }
        for (int i = 0; i < itemN; i++)
        {
            float rad = Mathf.Deg2Rad * angle * (i + rotI);
            Vector2 Tfunc = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            itemIcons[i].transform.localPosition = new Vector3(itemRingLength * Tfunc.y, itemRingLength * Tfunc.x, 0f);
            itemIcons[i].color = Offcolor;
            //itemIcons[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            //itemIconAnker.rotation = Quaternion.Euler(angle * Vector3.forward);
        }
        itemIcons[nowitem].color = selectColor;
        itemname.text = items[nowitem].itemname;
        //Debug.Log(items[nowitem].name);

    }
    // Start is called before the first frame update
    void Start()
    {
        //iconN = itemIcons.Count;
        //itemN = items.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateItem(-1);

            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RotateItem(+1);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                string id = items[nowitem].UseItem();
                Debug.Log("Use item " + id);
                //アイテムテキスト表示
                uic.ActiveUI(id);
            }
        }
    }
}
