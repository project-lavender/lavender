using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

//入手済みのアイテムのフラグ管理とアイテムＵＩ制御
public class ItemStack : MonoBehaviour
{
    [System.Serializable]
    struct ItemSet
    {
        public bool enable;
        public ItemStructure item;
        public Items itemComponent;
        public Image img;
    }
    [SerializeField] bool[] dbmode;
    [SerializeField] float itemUIdistance = 5f;
    [SerializeField] Transform itemIconAnker, itemSets;
    [SerializeField] GameObject itemIconPrefab;
    [SerializeField] UIcontroller uic;
    [SerializeField] FragTableHolder DTfrag;
    [SerializeField] ItemTableHolder DTitem;
    [SerializeField] List<ItemSet> itemList = new();
    [SerializeField] Sprite dummy;
    [SerializeField] TMP_Text itemname;
    [SerializeField] Color selectColor, Offcolor;

    [SerializeField] int nowitem = 0;

    private Lavender action;
    int EnableNum()
    {
        int i = 0;
        foreach (ItemSet set in itemList)
        {
            if (set.enable == true)
            {
                i++;
            }
        }
        return i;
    }
    void LineupItems()
    {
        int i = 0;
        foreach (ItemSet set in itemList)
        {
            //set　を配置
            set.img.transform.localPosition = new Vector3(itemUIdistance * i, 0f, 0f);
            set.img.color = Offcolor;
            //アイテムを入手した
            if (set.enable)
            {
                set.img.sprite = set.itemComponent.itemIcon;
                DTfrag.SetVal(set.itemComponent.myfrag, false);
            }
            else
            {
                //していないならダミー画像
                set.img.sprite = dummy;
                itemname.text = "???";
            }
            i++;
        }
        //選択中アイテム
        itemList[nowitem].img.color = selectColor;
        if (itemList[nowitem].enable)
        {
            itemname.text = itemList[nowitem].itemComponent.itemname;
            DTfrag.SetVal(itemList[nowitem].itemComponent.myfrag, true);

        }
    }
    public void DisableItem(string id)
    {
        //第一段階 idがItemList idに合致するか？ したらリストのフラグを折る
        if (id == "")
        {
            return;
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                ItemSet set = itemList[i];
                if (set.item.id == id)
                {
                    set.enable = false;
                    DTfrag.SetVal(set.itemComponent.myfrag, false);
                    itemList[i] = set;
                    break;
                }
            }
            //RotateItem(1);
            LineupItems();
        }
    }
    public void EnableItem(string id)
    {
        if (id == "")
        {
            return;
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                ItemSet set = itemList[i];
                if (set.item.id == id)
                {
                    set.enable = true;
                    itemList[i] = set;
                    Debug.Log(i);
                    DTfrag.SetVal(id, true);
                    RotateItem(i - nowitem);
                }
            }
            LineupItems();
        }
    }

    void RotateItem(int r)
    {
        if (EnableNum() == 0)
        {
            return;
        }
        //r...進める数
        int itemN = itemList.Count;

        if (nowitem == 0 && r < 0)
        {
            //nowitem 最前列 で後ろに行きたいときはリストの最後尾にジャンプ
            nowitem = itemN - 1;
        }
        else if (nowitem == itemN - 1 && r > 0)
        {
            //nowitem 最後尾 で前に行きたいときは初めにジャンプ
            nowitem = 0;
        }
        else
        {
            nowitem += r;
        }

    }
    // Start is called before the first frame update

    void Start()
    {
        int x = 0;
        foreach (ItemStructure dti in DTitem.ItemsList)
        {
            //オブジェを追加
            GameObject i = Instantiate(dti.item);
            i.transform.SetParent(itemSets);
            ItemStructure tItem = new();
            tItem.id = dti.id;
            tItem.item = i;
            //items.Add(tItem);
            //アイコンを追加
            Items itemComp = i.GetComponent<Items>();
            GameObject iconobj = Instantiate(itemIconPrefab, itemIconAnker);
            //iconobj.transform.parent = itemIconAnker;
            iconobj.transform.SetParent(itemIconAnker);
            //itemSets.SetParent(iconobj.transform);
            Image iconImg = iconobj.GetComponent<Image>();
            Sprite sprite = itemComp.itemIcon;
            iconImg.sprite = sprite;
            //itemIcons.Add(iconImg);

            ItemSet set = new();
            set.enable = false;
            set.item = tItem;
            set.img = iconImg;
            set.itemComponent = itemComp;
            itemList.Add(set);
            x++;
        }
        Debug.Log(EnableNum());
        LineupItems();
        action = new Lavender();
        
        x = 0;
        foreach (bool b in dbmode)
        {
            if (b)
            {
                ItemStructure i = DTitem.ItemsList[x];
                EnableItem(i.id);
            }
            x++;
        }
    }

    public void ItemNext(InputAction.CallbackContext context)
    {
        Debug.Log("aaaaaaa");
        if (context.performed)
        {
            RotateItem(1);
            LineupItems();
        }
    }

    public void ItemBack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RotateItem(-1);
            LineupItems();
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed && itemList[nowitem].enable)
        {
            Items itemComponent = itemList[nowitem].itemComponent;
            string id = itemComponent.UseItem();
            Debug.Log("Use item " + id);
            //アイテムテキスト表示

            uic.ActiveUI(id);
        }

    }
}
