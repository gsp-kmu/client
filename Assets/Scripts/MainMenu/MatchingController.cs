using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchingController : MonoBehaviour
{
    public GameObject matchingWindow;
    public LoadingAnimation loadingAnimation;
    public MatchingSoundController matchingSoundController;

    public TMPro.TextMeshProUGUI machingText;
    public TMPro.TextMeshProUGUI timeText;
    private const string matching_text = "매칭 중";
    private Action matchingEndCallback;

    private Coroutine matchingCoroutine;
    private Coroutine timeTextCoroutine;

    public void MatchingStart()
    {
        matchingWindow.gameObject.SetActive(true);
        gameObject.GetComponent<MatchingTipController>().StartTip();
        NetworkService.Instance.Send(NetworkEvent.MATCH_START, "");
        loadingAnimation.Play();
        matchingCoroutine = StartCoroutine(MatchingTextAnimation());
        timeTextCoroutine = StartCoroutine(MatchingTImeAnimation());
    }

    public void MatchingCancel()
    {
        NetworkService.Instance.Send(NetworkEvent.MATCH_CANCEL, "");
        StopCoroutine(matchingCoroutine);
        StopCoroutine(timeTextCoroutine);
        matchingWindow.gameObject.SetActive(false);
    }

    public void onMatchingSuccess()
    {
        //StopCoroutine(matchingCoroutine);
        //StopCoroutine(timeTextCoroutine);
        machingText.SetText("매칭 되었습니다.!!");


        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(machingText.rectTransform.DOScale(1.12f, 0.46f).SetEase(Ease.InQuad))
            .Append(machingText.rectTransform.DOScale(0.9f, 0.5f).SetEase(Ease.InQuad))
            .Append(machingText.rectTransform.DOScale(1.12f, 0.46f).SetEase(Ease.InQuad))
            .Append(machingText.rectTransform.DOScale(0.9f, 0.5f).SetEase(Ease.InQuad))
            .Append(machingText.rectTransform.DOScale(1.0f, 0.25f).SetEase(Ease.InQuad));
        sequence.Play();
        matchingSoundController.StartMatchingEnd();
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
            yield return null;
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
            yield return null;
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
