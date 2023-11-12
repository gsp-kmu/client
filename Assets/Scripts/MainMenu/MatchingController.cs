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
    private const string matching_text = "��Ī ��";
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
        machingText.SetText("��Ī�� �Ϸ�Ǿ����ϴ�.!");
    }

    public void onMatchingEnd()
    {
        machingText.SetText("���ӿ� �� ���� �մϴ�.!");
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
