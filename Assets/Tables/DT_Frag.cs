using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExcelAsset]
public class DT_Frag : ScriptableObject
{
	public List<DTFrag> Sheet1; // Replace 'EntityType' to an actual type that is serializable.


	public void SetVal(string fragname,bool val)
    {
        foreach(DTFrag f in Sheet1)
        {
            if (fragname == f.id)
            {
                f.value = val;
            }
        }
        //EditorUtility.SetDirty(this);
        //AssetDatabase.SaveAssets();
    }
    public bool ReadVal(string fragname)
    {
        bool val = false;
        foreach(DTFrag f in Sheet1)
        {
            if(fragname == f.id)
            {
                val = f.value;
            }
        }
        return val;
    }
    public void ResetValue()
    {
        foreach(DTFrag f in Sheet1)
        {
            f.value = false;
        }
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}
