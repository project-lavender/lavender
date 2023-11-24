using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoController : Gimicks
{
    //配電盤の店頭ランプ
    [SerializeField]
    private Color OffColor, OnColor;
    [SerializeField]
    private Light[] lights;
    [SerializeField]
    private Light[] execptionLights;
    private float[] intencities;
    [SerializeField]
    Material emmision;
    [SerializeField]
    private float maxOnTime = 0.5f;
    Material myMat;

    SEController se;
    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        myMat.SetColor("_RampColor", OffColor);
        lights = GameObject.FindObjectsOfType<Light>();
        intencities = new float[lights.Length];

        for(int i = 0; i < lights.Length; i++)
        {
            Light l = lights[i];
            intencities[i] = l.intensity;
            l.intensity = 0f;
        }
        for (int i = 0; i < execptionLights.Length; i++)
        {
            Light l = execptionLights[i];
            //intencities[i] = l.intensity;
            l.intensity = 0.5f;
        }

        //ライトのmaterial collor
        emmision.SetColor("_EmissionColor", Color.black);
        se = GetComponent<SEController>();
    }

    private IEnumerator LightTime(int i)
    {
        float rt = Random.Range(0f, maxOnTime);
        yield return new WaitForSeconds(rt);
        Light l = lights[i];
        l.intensity = intencities[i];

    }

    public override void DisableGimick()
    {
        base.DisableGimick();
    }
    //ダイナモon
    public override DTGimick InteractGimick()
    {
        base.InteractGimick();
        darkColor = Color.black;
        TurnOffColor();
        SetProgress();
        for (int i = 0; i < lights.Length; i++)
        {
            StartCoroutine(LightTime(i));
        }
        se.SE(0);
        myMat.SetColor("_RampColor", OnColor);
        emmision.SetColor("_EmissionColor", Color.white);
        return null;
    }
    
}
