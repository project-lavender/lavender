using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    public bool isRandom = true;
    [SerializeField] float pitchRangeMin = 0.7f,pitchRangeMax = 1.3f;
    [SerializeField] private AudioClip[] ses;
    [SerializeField] private AudioClip[] loopse;
    private AudioSource ass;

    public void SE(int i)
    {
        if (i < ses.Length && i > -1)
        {

            float pitch = Random.Range(pitchRangeMin, pitchRangeMax);
            if (!isRandom)
            {
                pitch = 1f;
            }
            ass.pitch = pitch;
            ass.PlayOneShot(ses[i]);
        }
    }

    public IEnumerator SetNoRandom(float t)
    {
        isRandom = false;
        yield return new WaitForSeconds(t);
        isRandom = true;
    }
    public void PitchReset()
    {
        ass.pitch = 1f;
    }
    public void SELoop(int i)
    {

        if (!ass.isPlaying && i < loopse.Length && i > -1)
        {
            ass.clip = loopse[i];
            ass.Play();
        }
    }
    public void LoopFinish()
    {
        ass.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
    }
}
