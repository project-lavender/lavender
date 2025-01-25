using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class GimickStructure
{
    public int progress = 0;
    public string frag = "";
    public string textID = "";
    public string demoID = "";
    public string itemID = "";
    public string upFrag = "";
    public string downFrag = "";
    public bool death = false;
    public int overWriteProgress = -1;
}
