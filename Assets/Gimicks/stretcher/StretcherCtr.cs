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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            SetProgress();
            rb.isKinematic = true;
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
