using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLogoRandomizer : MonoBehaviour
{

    [SerializeField] Sprite[] Logos;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float randomTime = 3f;
    Image img;
    int maxlength = 0;
    float t = 0f;
    private IEnumerator Randomizer()
    {
        t = 0f;
        while (t < randomTime)
        {
            
            int r = Random.Range(0, maxlength - 1);
            float wt = Random.Range(0.05f, waitTime);
            img.sprite = Logos[maxlength - 1];
            yield return new WaitForSeconds(0.05f);
            img.sprite = Logos[r];
            yield return new WaitForSeconds(wt);
            t += 0.1f + wt;

        }
        Debug.Log("finish rondom");
        while (true)
        {
            t += Time.deltaTime;
            float wt = Random.Range(0.3f, 1f);
            img.sprite = Logos[maxlength - 1];
            yield return new WaitForSeconds(0.1f);
            img.sprite = Logos[2];
            yield return new WaitForSeconds(wt);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        maxlength = Logos.Length;
        StartCoroutine(Randomizer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
