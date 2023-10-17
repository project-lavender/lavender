using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretcherCtr : Gimicks
{
    SEController se;
    Rigidbody rb;
    [SerializeField] float soundvelociy = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<SEController>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < soundvelociy)
        {
            se.LoopFinish();
        }
    }
    public override void InteractGimick()
    {
        //base.InteractGimick();
        Debug.Log("Interacted");
    }
    public override void EnableGimick()
    {
        base.EnableGimick();
        rb.isKinematic = false;
    }
    public override void DisableGimick()
    {
        base.DisableGimick();
        se = GetComponent<SEController>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            SetProgress();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(rb.velocity.magnitude);
        if (collision.gameObject.tag == "Player" && rb.velocity.magnitude > soundvelociy)
        {
            se.SELoop(0);
        }
        
    }
    
}
