using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class RierMover : MonoBehaviour
{
    [SerializeField]
    private TextAsset param;
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

    [SerializeField]
    private List<string[]> p = new();
    Light l;
    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
        l = GetComponentInChildren<Light>();
        Debug.Log(param.text);
        string ptext = param.text;
        string[] enterspl = ptext.Split("\n", System.StringSplitOptions.None);
        foreach (string s in enterspl){
            p.Add(s.Split(",", System.StringSplitOptions.None));
        }

        Debug.Log(p[1][1]);
        Debug.Log(p[2][1]);
        Debug.Log(p[3][1]);
        Debug.Log(p[4][1]);
        //PL_cam_height = float.Parse( p[1][1]);
        //PL_light_intencity = float.Parse( p[2][1]);
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
