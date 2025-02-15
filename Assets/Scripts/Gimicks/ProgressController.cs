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
    [SerializeField] FragTableHolder frag = null;

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
            case 120:
                foreach (Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                }
                break;
            case 300:
                foreach (Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                }
                break;
            case 320:
                foreach (Gimicks g in allgimicks)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                }
                break;
            case 330:
                foreach (Gimicks g in allgimicks)
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

                //初めはダイナモとストレッチャーだけ起動
                if (g == dynamo || g == stretcher)
                {
                    g.EnableGimick();
                    g.TurnOffColor();
                    
                }
            }
        }
        if (frag != null && !debugMode)
        {
            frag.ResetValue();
        }
    }
}
