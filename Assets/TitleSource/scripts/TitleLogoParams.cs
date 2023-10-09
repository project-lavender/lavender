using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLogoParams : MonoBehaviour
{
    public float Grad = 15f;
    [SerializeField] Sprite[] Logos;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float randomTime = 3f;
    [SerializeField] Image bc;

    Material bcMat;

    int maxlength = 0;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        bcMat = bc.material;
    }

    private void Update()
    {
        bcMat.SetFloat("_Grad", Grad);
    }
}
