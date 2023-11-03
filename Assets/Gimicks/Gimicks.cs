using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimicks : MonoBehaviour
{

    [SerializeField] int p;
    ProgressController prog;

    [SerializeField]
    private List<Material> myMats;

    [SerializeField] private string gimicksName;
    [SerializeField] private DT_Gimicks gimicks;
    [SerializeField] private DT_Frag frags;
    [SerializeField] private List<DTGimick> gimickDatalist;
    [SerializeField]
    private Color emittionColor,darkColor;

    private Renderer[] renderers;
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
        prog = GameObject.FindGameObjectWithTag("GameController").GetComponent<ProgressController>();
        prog.AttachProgress(p);
    }
    public virtual DTGimick InteractGimick()
    {
        Debug.Log("Base Interact" + gameObject.name);
        //string textid = "";
        DTGimick ret = null;
        foreach (DTGimick g in gimickDatalist)
        {
            //進行度とフラグが立っているなら実行
            Debug.Log(frags.ReadVal(g.frag));
            if (g.progress == prog.ReadProgress() && frags.ReadVal(g.frag))
            {
                Debug.Log("Do Event " + gameObject.name);
                //textid = g.textID;
                frags.SetVal(g.upFrag, true);
                frags.SetVal(g.downFrag, false);
                ret = g;
                break;
            }
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
    }
}
