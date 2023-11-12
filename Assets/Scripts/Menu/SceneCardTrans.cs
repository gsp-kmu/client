using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCardTrans : MonoBehaviour
{
    public Animator animator;
    public bool isFirstStart;

    private void Start()
    {
        if(isFirstStart == true)
        {
            FadeOut();
        }
    }
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
