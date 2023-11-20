using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextFade : MonoBehaviour
{
    public float fadeInDuration;
    public float fadeOutDuration;
    public float delayBetweenFades;

    public TextMeshProUGUI text;

    void Start()
    {
        FadeIn();
    }

    void FadeIn()
    {
        text.DOFade(1.0f, fadeInDuration)
            .OnComplete(() => {
                Invoke("FadeOut", delayBetweenFades);
            });
    }

    void FadeOut()
    {
        text.DOFade(0.0f, fadeOutDuration)
            .OnComplete(() => {
                Invoke("FadeIn", delayBetweenFades);
            });
    }
}