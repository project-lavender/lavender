using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExcelAsset]
public class GimickTableHolder : ScriptableObject
{
	public List<GimickStructure> Stretcher; // Replace 'EntityType' to an actual type that is serializable.
    public List<GimickStructure> Dynamo;
    public List<GimickStructure> TestGimick;
    public List<GimickStructure> TestGimick_choise;
    public List<GimickStructure> TestGimick_voice;
    public List<GimickStructure> CassetteTapeA;
    public List<GimickStructure> CassetteTapeB;
    public List<GimickStructure> CassetteTapeC;
    public List<GimickStructure> CassetteTapeD;
    public List<GimickStructure> KasetPlayer;
    public List<GimickStructure> MemoO;
    public List<GimickStructure> MemoA;
    public List<GimickStructure> MemoB;
    public List<GimickStructure> MemoC;
    public List<GimickStructure> MemoD;
    public List<GimickStructure> RoterRed;
    public List<GimickStructure> RoterBlack;
    public List<GimickStructure> RoterBlue;
    public List<GimickStructure> Freezer;
    public List<GimickStructure> DimpleKey;
    public List<GimickStructure> Centrifuge;
    public List<GimickStructure> CentrifugeM;
    public List<GimickStructure> Pipet;
    public List<GimickStructure> Door;
    public List<GimickStructure> ReagentBottle;
    public List<GimickStructure> PipetManual;
    public List<GimickStructure> CentrifugeManual;
    public List<GimickStructure> FakeReagentBottle;

    public List<GimickStructure> FindTableENUM(ENUMGimickName.GimickNames listname)
    {
        List<GimickStructure> ret = null;
        if (listname.ToString() == nameof(Stretcher))
        {
            ret = Stretcher;
        }
        else if (listname.ToString() == nameof(Dynamo))
        {
            ret = Dynamo;
        }
        else if (listname.ToString() == nameof(TestGimick))
        {
            ret = TestGimick;
        }
        else if (listname.ToString() == nameof(TestGimick_choise))
        {
            ret = TestGimick_choise;
        }
        else if (listname.ToString() == nameof(TestGimick_voice))
        {
            ret = TestGimick_voice;
        }
        else if (listname.ToString() == nameof(CassetteTapeA))
        {
            ret = CassetteTapeA;
        }
        else if (listname.ToString() == nameof(CassetteTapeB))
        {
            ret = CassetteTapeB;
        }
        else if (listname.ToString() == nameof(CassetteTapeC))
        {
            ret = CassetteTapeC;
        }
        else if (listname.ToString() == nameof(CassetteTapeD))
        {
            ret = CassetteTapeD;
        }
        else if (listname.ToString() == nameof(KasetPlayer))
        {
            ret = KasetPlayer;
        }
        else if (listname.ToString() == nameof(MemoO))
        {
            ret = MemoO;
        }
        else if (listname.ToString() == nameof(MemoA))
        {
            ret = MemoA;
        }
        else if (listname.ToString() == nameof(MemoB))
        {
            ret = MemoB;
        }
        else if (listname.ToString() == nameof(MemoC))
        {
            ret = MemoC;
        }
        else if (listname.ToString() == nameof(MemoD))
        {
            ret = MemoD;
        }
        else if (listname.ToString() == nameof(RoterRed))
        {
            ret = RoterRed;
        }
        else if (listname.ToString() == nameof(RoterBlack))
        {
            ret = RoterBlack;
        }
        else if (listname.ToString() == nameof(RoterBlue))
        {
            ret = RoterBlue;
        }
        else if (listname.ToString() == nameof(Freezer))
        {
            ret = Freezer;
        }
        else if (listname.ToString() == nameof(DimpleKey))
        {
            ret = DimpleKey;
        }
        else if (listname.ToString() == nameof(Centrifuge))
        {
            ret = Centrifuge;
        }
        else if (listname.ToString() == nameof(CentrifugeM))
        {
            ret = CentrifugeM;
        }
        else if (listname.ToString() == nameof(Pipet))
        {
            ret = Pipet;
        }
        else if (listname.ToString() == nameof(Door))
        {
            ret = Door;
        }
        else if (listname.ToString() == nameof(ReagentBottle))
        {
            ret = ReagentBottle;
        }
        else if (listname.ToString() == nameof(PipetManual))
        {
            ret = PipetManual;
        }
        else if (listname.ToString() == nameof(CentrifugeManual))
        {
            ret = CentrifugeManual;
        }
        else if (listname.ToString() == nameof(FakeReagentBottle))
        {
            ret = FakeReagentBottle;
        }
        else
        {
            return null;
        }
        return ret;
    }
}
