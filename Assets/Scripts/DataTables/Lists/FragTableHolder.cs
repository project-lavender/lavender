using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExcelAsset]
public class FragTableHolder : ScriptableObject
{
	public List<FragStructure> Sheet1; // Replace 'EntityType' to an actual type that is serializable.


    public void SetVal(string fragname, bool val)
    {
        foreach (FragStructure f in Sheet1)
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
        foreach(FragStructure f in Sheet1)
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
        foreach(FragStructure f in Sheet1)
        {
            f.value = false;
        }
        //EditorUtility.SetDirty(this);
        //AssetDatabase.SaveAssets();
    }
}
