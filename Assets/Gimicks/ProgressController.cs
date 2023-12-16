using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{

    //progressによってアクティブなギミックを設定
    [SerializeField]private int progress = 0;
    [SerializeField] bool debugMode = false;
    [SerializeField] Gimicks dynamo = null, stretcher=null;
    [SerializeField] DT_Frag frag = null;

    [SerializeField] Gimicks[] allgimicks = null;

    public int ReadProgress()
    {
        return progress;
    }
    public void UpFrag(string id)
    {
        frag.SetVal(id, true);
    }
    public void DownFrag(string id)
    {
        frag.SetVal(id, false);
    }
    public void AttachProgress(int p)
    {
        Debug.Log("AttachProgress");
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
                //ストレッチャーとダイナモは明かりを消す
                foreach (Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                }

                dynamo.darkColor = Color.black;
                dynamo.DisableGimick();
                stretcher.darkColor = Color.black;
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
                g.BlackColor();
                if (g == dynamo || g == stretcher)
                {
                    Debug.Log("aaaa");
                    g.EnableGimick();
                    g.TurnOffColor();
                    
                }
            }
        }
        if (frag != null)
        {
            frag.ResetValue();
        }

    }
}
