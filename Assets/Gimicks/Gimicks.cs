using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimicks : MonoBehaviour
{

    [SerializeField] int p;
    ProgressController prog;

    [SerializeField]
    private List<Material> myMats;

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
    public virtual void InteractGimick()
    {
        Debug.Log("Base Interact" + gameObject.name);
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
