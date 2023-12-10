using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    public List<Animator> loadingAnimator;
    private void Start()
    {
    }
    public void Play()
    {
        StartCoroutine(PlayAnimation());
    }
    public void Stop()
    {
        StopAnimation();
    }
    private IEnumerator PlayAnimation()
    {
        yield return null;
        for (int i = 0; i < loadingAnimator.Count; i++)
        {
            loadingAnimator[i].SetBool("isLoading", true);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void StopAnimation()
    {
        for (int i = 0; i < loadingAnimator.Count; i++)
        {
            loadingAnimator[i].SetBool("isLoading", false);
        }
    }
}
