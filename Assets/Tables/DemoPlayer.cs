using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DemoPlayer : MonoBehaviour
{
    [SerializeField] PlayableDirector[] directors;

    public void DemoPlay(string name)
    {
        foreach(PlayableDirector d in directors)
        {
            if(d.name == name)
            {
                d.Play();
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
    // Start is called before the first frame update
    void Start()
    {
        directors = GetComponentsInChildren<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
