using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    //左クリックからインタラクトできる時間
    [SerializeField] float iTime = 0.2f;
    private bool interactTrigger = false;

    // Start is called before the first frame update
    [SerializeField]
    Gimicks gimicks = null;
    IEnumerator InteractTrigger()
    {
        interactTrigger = true;
        yield return new WaitForSeconds(iTime);
        interactTrigger = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(InteractTrigger());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gimick")
        {
            gimicks = other.GetComponent<Gimicks>();
            if (gimicks != null)
            {
                gimicks.EmitColor();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Gimick" && interactTrigger)
        {
            interactTrigger = false;
            
            if (gimicks != null)
            {
                gimicks.InteractGimick();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gimicks != null)
        {
            gimicks.TurnOffColor();
            gimicks = null;
        }
    }
}
