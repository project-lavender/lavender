using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{

    //progressによってアクティブなギミックを設定
    [SerializeField]private int progress = 0;
    [SerializeField]
    Gimicks dynamo, stretcher;

    [SerializeField] Gimicks[] allgimicks;

    public void AttachProgress(int p)
    {
        progress = p;
        switch (p)
        {
            case 100:
                //active stretcher
                dynamo.DisableGimick();
                stretcher.EnableGimick();
                break;

            case 110:
                //active all gimicks
                foreach(Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                }
                dynamo.DisableGimick();
                stretcher.DisableGimick();
                break;

            default:
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        allgimicks = FindObjectsOfType<Gimicks>();
        foreach(Gimicks g in allgimicks)
        {
            if (g != dynamo)
            {
                g.DisableGimick();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
