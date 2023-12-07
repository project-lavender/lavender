using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DT_Gimicks : ScriptableObject
{
	public List<DTGimick> Stretcher; // Replace 'EntityType' to an actual type that is serializable.
    public List<DTGimick> Dynamo;
    public List<DTGimick> TestGimick;
    public List<DTGimick> TestGimick_choise;
    public List<DTGimick> TestGimick_voice;
    public List<DTGimick> CassetteTapeA;
    public List<DTGimick> CassetteTapeB;
    public List<DTGimick> CassetteTapeC;
    public List<DTGimick> CassetteTapeD;
    public List<DTGimick> KasetPlayer;
    public List<DTGimick> MemoO;
    public List<DTGimick> MemoA;
    public List<DTGimick> MemoB;
    public List<DTGimick> MemoC;
    public List<DTGimick> MemoD;
    public List<DTGimick> RoterRed;
    public List<DTGimick> RoterBlack;
    public List<DTGimick> RoterBlue;
    public List<DTGimick> Freezer;
    public List<DTGimick> DimpleKey;
    public List<DTGimick> Centrifuge;

    public List<DTGimick> FindTable(string listname)
    {
        if (listname == nameof(Stretcher))
        {
            return Stretcher;
        }
        else if (listname == nameof(Dynamo))
        {
            return Dynamo;
        }
        else if (listname == nameof(TestGimick))
        {
            return TestGimick;
        }
        else if (listname == nameof(TestGimick_choise))
        {
            return TestGimick_choise;
        }
        else if (listname == nameof(TestGimick_voice))
        {
            return TestGimick_voice;
        }
        else if (listname == nameof(CassetteTapeA))
        {
            return CassetteTapeA;
        }
        else if (listname == nameof(CassetteTapeB))
        {
            return CassetteTapeB;
        }
        else if (listname == nameof(CassetteTapeC))
        {
            return CassetteTapeC;
        }
        else if (listname == nameof(CassetteTapeD))
        {
            return CassetteTapeD;
        }
        else if (listname == nameof(KasetPlayer))
        {
            return KasetPlayer;
        }
        else if (listname == nameof(MemoO))
        {
            return MemoO;
        }
        else if (listname == nameof(MemoA))
        {
            return MemoA;
        }
        else if (listname == nameof(MemoB))
        {
            return MemoB;
        }
        else if (listname == nameof(MemoC)) {
            return MemoC;
        }
        else if (listname == nameof(MemoD))
        {
            return MemoD;
        }
        else if (listname == nameof(RoterRed))
        {
            return RoterRed;
        }
        else if (listname == nameof(RoterBlack))
        {
            return RoterBlack;
        }
        else if (listname == nameof(RoterBlue))
        {
            return RoterBlue;
        }
        else if (listname == nameof(Freezer))
        {
            return Freezer;
        }
        else if(listname == nameof(DimpleKey))
        {
            return DimpleKey;
        }
        else if(listname == nameof(Centrifuge))
        {
            return Centrifuge;
        }
        else
        {
            return null;
        }
    }

}
