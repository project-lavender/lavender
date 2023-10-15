using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimicks : MonoBehaviour
{
    [SerializeField] int p;
    ProgressController prog;
    public void SetProgress()
    {
        prog = GameObject.FindGameObjectWithTag("GameController").GetComponent<ProgressController>();
        prog.progress = p;
    }
    public virtual void InteractGimick()
    {
        Debug.Log("Base Interact");
    }
    // Start is called before the first frame update
    void Start()
    {
        prog = GameObject.FindGameObjectWithTag("GameController").GetComponent<ProgressController>();
    }
}
