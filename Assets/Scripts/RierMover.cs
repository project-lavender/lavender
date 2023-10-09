using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RierMover : MonoBehaviour
{
    private CharacterController ctr;
    [SerializeField]
    private float PL_cam_height = 2.2f;
    [SerializeField]
    private float PL_light_intencity = 10f;
    [SerializeField]
    private float PL_walk = 3.0f;
    [SerializeField]
    private float PL_run = 6.0f;
    
    private float g = 9.8f;

    private float h, v, c;


    Light l;
    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
        l = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        ctr.height = PL_cam_height;
        l.intensity = PL_light_intencity;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            c = PL_run;
        }
        else
        {
            c = PL_walk;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        ctr.Move((Camera.main.transform.forward * v +Camera.main.transform.right * h + Vector3.down * g) * c * Time.deltaTime);
    }
}
