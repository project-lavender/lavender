using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleLogoParams : MonoBehaviour
{
    public float Grad = 15f;
    [SerializeField] Image bc = null;

    Material bcMat;


    public void MoveScene(int i)
    {
        SceneManager.LoadScene(i);
    }

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
