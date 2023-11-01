using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StretcherCtr : Gimicks
{
    SEController se;
    Rigidbody rb;
    PlayableDirector director;
    [SerializeField] float soundvelociy = 0.1f;

    //[SerializeField] private DT_Stretcher stretcher = null;
    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<SEController>();
        rb = GetComponent<Rigidbody>();
        director = GetComponent<PlayableDirector>();
    }
    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < soundvelociy)
        {
            se.LoopFinish();
        }
    }
    /*
    public override void InteractGimick()
    {
        //base.InteractGimick();
        Debug.Log("Interacted");
        director.Play();
    }
    */
    public override void EnableGimick()
    {
        base.EnableGimick();
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

            rb.isKinematic = false;
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
