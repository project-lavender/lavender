using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Image blood;
    [SerializeField] float amp = 1f, freq = 0.5f;
    [SerializeField] Color bloodColor,ccolor;
    float t = 0f;
    public void MoveScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void FinishGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        t += Time.deltaTime;
        blood.color = bloodColor * amp * abs(noise.snoise(float2(0f, t * freq))) + ccolor;
    }





}
