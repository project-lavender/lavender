using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour
{

    //progressによってアクティブなギミックを設定
    [SerializeField]private int progress = 0;
    [SerializeField] bool debugMode = false;
    [SerializeField] Gimicks dynamo = null, stretcher = null, door = null;
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
    public void SceneLoad(string scenename)
    {

        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(scenename);
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

                //dynamo.darkColor = Color.black;
                //dynamo.BlackColor();
                //dynamo.DisableGimick();
                //stretcher.darkColor = Color.black;
                //0stretcher.BlackColor();
                //stretcher.DisableGimick();

                
                break;
            case 320:
                //ストレッチャーとダイナモは明かりを消す
                foreach (Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                }                
                break;
            case 330:
                foreach(Gimicks g in allgimicks)
                {
                    g.BlackColor();
                    g.DisableGimick();
                }

                dynamo.TurnOffColor();
                dynamo.EnableGimick();
                stretcher.EnableGimick();
                stretcher.TurnOffColor();
                door.EnableGimick();
                door.TurnOffColor();

                break;
            case 500:
                dynamo.TurnOffColor();
                dynamo.EnableGimick();
                stretcher.TurnOffColor();
                stretcher.EnableGimick();
                break;
            default:
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
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
