using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchingSoundController : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource audioSouce;
    public List<AudioClip> matchingClip;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartMatchingEnd();
        }
    }

    public void StartMatchingEnd()
    {
        bgmSource.DOFade(0.0f, 0.5f);
        Invoke("StartMatchingEndInvoke", 0.5f);
    }

    private void StartMatchingEndInvoke()
    {
        for (int i = 0; i < matchingClip.Count; i++)
        {
            audioSouce.PlayOneShot(matchingClip[i]);
        }
    }
}
