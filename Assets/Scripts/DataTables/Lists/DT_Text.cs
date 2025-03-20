using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DT_Text : ScriptableObject
{
	public List<TextStructure> Sheet1; // Replace 'EntityType' to an actual type that is serializable.

	public TextStructure Find(string id)
    {
        TextStructure text = null;
        foreach (TextStructure t in Sheet1)
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
        TextStructure text = Find(id);
        if (text == null)
        {
            return (-1, null);
        }
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
