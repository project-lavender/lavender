using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class VaseLightRandomizer : MonoBehaviour
{
    [SerializeField] float amp = 0.5f, freq = 1f, c = 0.01f;
    private float t = 0f;
    private Light lit;
    // Start is called before the first frame update
    void Start()
    {
        lit = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        lit.intensity = amp * abs(noise.snoise(float2(0f, t * freq))) + c;
    }
}
