using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class DemoPlayer : MonoBehaviour
{
    [SerializeField] PlayableDirector[] directors;
    private Lavender action;
    [SerializeField] PlayableDirector director;

    public void DemoPlay(string name)
    {
        foreach(PlayableDirector d in directors)
        {
            if(d.name == name)
            {

                d.Play();
                director = d;
                var rootP = director.playableGraph.GetRootPlayable(0);
                rootP.SetSpeed(1f);
            }
        }
    }
    public void StopDemo(string name)
    {
        foreach (PlayableDirector d in directors)
        {
            if (d.name == name)
            {
                d.Stop();
            }
        }
    }

    void Pause(InputAction.CallbackContext callback)
    {
        Debug.Log("pause demo");
        if (director == null || director.state == PlayState.Paused)
        {
            return;
        }
        var rootP = director.playableGraph.GetRootPlayable(0);
        if (rootP.GetSpeed() == 1f)
        {
            rootP.SetSpeed(0f);
        }
        else
        {
            rootP.SetSpeed(1f);
        }

    }
    void Skip(InputAction.CallbackContext callback)
    {
        if(director == null || director.state == PlayState.Paused)
        {
            return;
        }
        var rootP = director.playableGraph.GetRootPlayable(0);
        rootP.SetSpeed(10f);
    }
    // Start is called before the first frame update
    void Start()
    {
        directors = GetComponentsInChildren<PlayableDirector>();
        action = new Lavender();
        action.UI.Pause.started += Pause;
        action.UI.Skip.started += Skip;
        action.Enable();
    }
    

}
