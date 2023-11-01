using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DT_Gimicks : ScriptableObject
{
	public List<DTGimick> Stretcher; // Replace 'EntityType' to an actual type that is serializable.
    public List<DTGimick> TestGimick;

	public List<DTGimick> FindTable(string listname)
    {
        if (listname == nameof(Stretcher))
        {
            return Stretcher;
        }
        else if(listname == nameof(TestGimick) || listname == "")
        {
            return TestGimick;
        }
        else
        {
            return null;
        }
    }

}
