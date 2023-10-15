using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLorder : MonoBehaviour
{
    public void LoadScene(string lname)
    {
        SceneManager.LoadScene(lname);
    }
}
