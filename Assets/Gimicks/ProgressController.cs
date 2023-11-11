using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{

    //progressによってアクティブなギミックを設定
    [SerializeField]private int progress = 0;
    [SerializeField] bool debugMode;
    [SerializeField] Gimicks dynamo, stretcher;

    [SerializeField] Gimicks[] allgimicks;

    public int ReadProgress()
    {
        return progress;
    }

    public void AttachProgress(int p)
    {
        progress = p;
        switch (p)
        {
            case 100:
                //active stretcher
                //dynamo.DisableGimick();
                //stretcher.EnableGimick();
                break;

            case 110:
                //active all gimicks
                
                //dynamo.DisableGimick();
                //stretcher.DisableGimick();
                break;
            case 120:
                foreach (Gimicks g in allgimicks)
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
        if (!debugMode)
        {
            allgimicks = FindObjectsOfType<Gimicks>();
            foreach (Gimicks g in allgimicks)
            {
                g.DisableGimick();
                if (g == dynamo || g == stretcher)
                {
                    Debug.Log("aaaa");
                    g.EnableGimick();
                }
            }
        }

    }
}
