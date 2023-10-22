using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DTGimick : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public struct Table
    {
        public int progress;
        public bool frag;
        public TextAsset text;
        public PlayableAsset demo;
        public Items item;
        public bool[] upFrag;
        public bool[] downFrag;
    }

    [SerializeField] Table[] tables = new Table[3];
}
