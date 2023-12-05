using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCardTrans : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;
    public bool isFirstStart;

    private void Start()
    {
        if (isFirstStart == true)
        {
            FadeOut();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        audioSource.Play();
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        audioSource.Play();
        animator.SetTrigger("FadeOut");
    }
}
