using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoController : Gimicks
{
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
    Color offcolor, oncollor;
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
            emmision.SetColor("_EmissionColor", offcolor);
        }
        for (int i = 0; i < execptionLights.Length; i++)
        {
            Light l = execptionLights[i];
            //intencities[i] = l.intensity;
            l.intensity = 0.5f;
        }

        se = GetComponent<SEController>();
    }

    private IEnumerator LightTime(int i)
    {
        float rt = Random.Range(0f, maxOnTime);
        yield return new WaitForSeconds(rt);
        Light l = lights[i];
        l.intensity = intencities[i];

    }

    
    //ƒ_ƒCƒiƒ‚on
    public override DTGimick InteractGimick()
    {

        myMat.SetColor("_RampColor", OnColor);
        SetProgress();
        for (int i = 0; i < lights.Length; i++)
        {
            StartCoroutine(LightTime(i));
        }
        emmision.SetColor("_EmissionColor", oncollor);
        se.SE(0);
        return null;
    }
    
}
