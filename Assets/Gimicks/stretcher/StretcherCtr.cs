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
        base.DisableGimick();
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
    public override void DisableGimick()
    {
        base.DisableGimick();
        darkColor = Color.black;
        TurnOffColor();
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            base.InteractGimick();
            base.EnableGimick();
            SetProgress();
            rb.isKinematic = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //動かしている間　ストレッチャーから音が鳴る
        if (collision.gameObject.tag == "Player" && rb.velocity.magnitude > soundvelociy)
        {
            se.SELoop(0);
        }
        
    }
    
}
