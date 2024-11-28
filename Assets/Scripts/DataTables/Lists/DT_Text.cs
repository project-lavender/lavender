using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DT_Text : ScriptableObject
{
	public List<DTText> Sheet1; // Replace 'EntityType' to an actual type that is serializable.

	public
        DTText Find(string id)
    {
        DTText text = null;
        foreach (DTText t in Sheet1)
        {
            if(id == t.id)
            {
                text = t;
            }
        }
        return text;
    }

    public (int,List<string>) Pages(string id)
    {
        DTText text = Find(id);
        int pagesize = 1;
        List<string> texts = new();
        texts.Add(text.text);
        while (text.nextText != "")
        {
            text = Find(text.nextText);
            texts.Add(text.text);
            pagesize += 1;
        }
        return (pagesize,texts);
    }
}
