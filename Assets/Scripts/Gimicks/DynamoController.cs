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
    [SerializeField] private float[] intencities,exceIntencity;
    [SerializeField] Material emmision;
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
        exceIntencity = new float[execptionLights.Length];
        for (int i = 0; i < execptionLights.Length; i++)
        {

            Light l = execptionLights[i];
            exceIntencity[i] = l.intensity;

        }
        for (int i = 0; i < lights.Length; i++)
        {
            Light l = lights[i];
            intencities[i] = l.intensity;
            l.intensity = 0f;
        }
        for (int i = 0; i < execptionLights.Length; i++)
        {

            Light l = execptionLights[i];
            l.intensity = exceIntencity[i];

        }

        //ライトのmaterial collor
        emmision.SetColor("_EmissionColor", Color.black);
        se = GetComponent<SEController>();
    }

    private IEnumerator LightTime(int i,bool on)
    {
        float rt = Random.Range(0f, maxOnTime);
        yield return new WaitForSeconds(rt);
        Light l = lights[i];
        if (on)
        {
            l.intensity = intencities[i];
        }
        else
        {
            l.intensity = 0f;
            foreach (Light el in execptionLights)
            {
                if (el == l)
                {
                    l.intensity = intencities[i];
                }
            }
        }

    }
    public void TurnOffLight()
    {
        
        se.SE(0);

        emmision.SetColor("_EmissionColor", Color.black);
        myMat.SetColor("_RampColor", OffColor);
        for (int i = 0; i < lights.Length; i++)
        {
            
            StartCoroutine(LightTime(i, false));
        }
    }
    public void TurnOnLight()
    {
        Debug.Log("ON Light");

        emmision.SetColor("_EmissionColor", Color.white);
        se.SE(0);
        myMat.SetColor("_RampColor", OnColor);
        for (int i = 0; i < lights.Length; i++)
        {
            StartCoroutine(LightTime(i, true));
        }
    }
    //ダイナモon
    
}
