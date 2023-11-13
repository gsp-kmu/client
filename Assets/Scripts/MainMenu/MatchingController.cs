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
    public TMPro.TextMeshProUGUI timeText;
    private const string matching_text = "매칭 중";
    private Action matchingEndCallback;

    public void MatchingStart()
    {
        matchingWindow.gameObject.SetActive(true);
        NetworkService.Instance.Send(NetworkEvent.MATCH_START, "");
        loadingAnimation.Play();
        StartCoroutine(MatchingTextAnimation());
        StartCoroutine(MatchingTImeAnimation());
    }

    public void MatchingCancel()
    {
        NetworkService.Instance.Send(NetworkEvent.MATCH_CANCEL, "");
        StopCoroutine(MatchingTextAnimation());
        StopCoroutine(MatchingTImeAnimation());
        matchingWindow.gameObject.SetActive(false);
    }

    public void onMatchingSuccess()
    {
        machingText.SetText("매칭 되었습니다.!!");
    }

    public void onMatchingEnd()
    {
        machingText.SetText("게임을 곧 시작합니다.!");
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
                machingText.SetText(matching_text + new string('.', i));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    IEnumerator MatchingTImeAnimation()
    {
        yield return null;
        int time = 0;
        SetTImeText(time);

        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            time += 1;
            SetTImeText(time);
        }
    }

    private void SetTImeText(int time)
    {
        int sec = time % 60;
        int minute = time / 60;

        string timeString = "";
        timeString += SetTimeToText(minute);
        timeString += ":" + SetTimeToText(sec);
        Debug.Log(timeString);
        timeText.SetText(timeString);
    }

    private string SetTimeToText(int time){
        if(time == 0)
            return "00";
        else if(time /10>= 1)
            return "" + time;

        return "0" + time;
    }
}
