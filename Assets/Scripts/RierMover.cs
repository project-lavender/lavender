using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RierMover : MonoBehaviour
{
    private CharacterController ctr;

    [SerializeField]
    private float walkspeed = 3.0f;
    [SerializeField]
    private float runspeed = 6.0f;
    private float g = 9.8f;

    private float h, v, c;

    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            c = runspeed;
        }
        else
        {
            c = walkspeed;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        ctr.Move((Camera.main.transform.forward * v +Camera.main.transform.right * h + Vector3.down * g) * c * Time.deltaTime);
    }
}
