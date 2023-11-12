using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchingController : MonoBehaviour
{
    public GameObject matchingWindow;
    public LoadingAnimation loadingAnimation;
    public TMPro.TextMeshProUGUI machingText;
    private const string matching_text = "매칭 중";
    private Action matchingEndCallback;

    public void MatchingStart()
    {
        matchingWindow.gameObject.SetActive(true);
        NetworkService.Instance.Send(NetworkEvent.MATCH_START, "");
        loadingAnimation.Play();
        StartCoroutine(MatchingTextAnimation());
    }

    public void MatchingCancel()
    {
        NetworkService.Instance.Send(NetworkEvent.MATCH_CANCEL, "");
        StopCoroutine(MatchingTextAnimation());
        matchingWindow.gameObject.SetActive(false);
    }

    public void onMatchingSuccess()
    {
        machingText.SetText("매칭이 완료되었습니다.!");
    }

    public void onMatchingEnd()
    {
        machingText.SetText("게임에 곧 접속 합니다.!");
        Invoke("MoveInGameScene", 0.8f);
    }

    public void InitCallbackMathicngEnd(Action callback)
    {
        matchingEndCallback = callback;
    }

    private void MoveInGameScene()
    {
        matchingEndCallback();
    }

    IEnumerator MatchingTextAnimation()
    {
        yield return null;
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("hi");
                machingText.SetText(matching_text + new string('.', i));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
