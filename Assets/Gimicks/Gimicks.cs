using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimicks : MonoBehaviour
{
    //[SerializeField] bool isDeath_if_Use = false;
    //[SerializeField] int p = 0;
    ProgressController prog;

    [SerializeField]
    private List<Material> myMats = null;

    [SerializeField] private string gimicksName = "";
    [SerializeField] private DT_Gimicks gimicks = null;
    [SerializeField] private DT_Frag frags = null;
    [SerializeField] private List<DTGimick> gimickDatalist;
    public Color emittionColor = Color.white, darkColor = Color.black;

    private Renderer[] renderers;
    //private ItemStack itemStack;
    public void EmitColor()
    {
        foreach(Material mat in myMats)
        {
            mat.SetColor("_EmissionColor", emittionColor);
        }
    }
    public void TurnOffColor()
    {
        foreach (Material mat in myMats)
        {
            mat.SetColor("_EmissionColor", darkColor);
        }
    }
    public void SetProgress()
    {
        //prog = GameObject.FindGameObjectWithTag("GameController").GetComponent<ProgressController>();
        //prog.AttachProgress(p);
    }
    public virtual DTGimick InteractGimick()
    {
        Debug.Log("Base Interact" + gameObject.name);
        //string textid = "";
        DTGimick ret = null;
        if (gimickDatalist == null)
        {
            return null;
        }
        foreach (DTGimick g in gimickDatalist)
        {
            //進行度とフラグが立っているなら実行
            Debug.Log(frags.ReadVal(g.frag));
            bool frag;
            if (g.frag == "")
            {
                frag = true;
            }
            else
            {
                frag = frags.ReadVal(g.frag);
            }

            //progress = -1 ならいつでもOK
            if ((g.progress == -1 || g.progress == prog.ReadProgress()) && frag)
            {
                Debug.Log("Do Event " + gameObject.name);
                //textid = g.textID;
                frags.SetVal(g.upFrag, true);
                frags.SetVal(g.downFrag, false);
                int p = g.overWriteProgress;
                if (p != -1)
                {
                    prog.AttachProgress(p);
                }
                ret = g;
                break;
            }
            
        }
        if (ret.death)
        {
            Destroy(gameObject);
        }
        return ret;
    }
    public virtual void DisableGimick()
    {
        Debug.Log("Base DisableGimick "+gameObject.name);
        gameObject.tag = "Untagged";
    }
    public virtual void EnableGimick()
    {
        Debug.Log("Base EnableGimick" + gameObject.name);
        gameObject.tag = "Gimick";
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (gimicks != null)
        {
            gimickDatalist = gimicks.FindTable(gimicksName);
        }
        prog = GameObject.FindGameObjectWithTag("GameController").GetComponent<ProgressController>();
        renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            foreach(Material m in r.materials)
            {
                myMats.Add(m);
            }
        }
        TurnOffColor();
    }
}
