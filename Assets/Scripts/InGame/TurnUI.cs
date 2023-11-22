using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI turnText;
    public RectTransform rectTransform;

    public void StartMyTurnAnimation(int turn)
    {
        rectTransform.localScale = Vector3.zero;
        turnText.text = "ео " + turn + " / 20";

        Sequence sequence = DOTween.Sequence()
            .Append(rectTransform.DOScale(1.0f, 0.15f))
            .Append(rectTransform.DOScale(0.0f, 0.5f).SetDelay(1.0f))
            .AppendCallback(()=>
            {
                gameObject.SetActive(false);
            });

        sequence.Play();
    }
}
