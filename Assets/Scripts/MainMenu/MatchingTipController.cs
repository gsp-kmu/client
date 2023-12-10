using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class TipBox
{
    public Sprite sprite;
    public Color color;
}

public class MatchingTipController : MonoBehaviour
{
    public List<TipBox> tipBox;
    public List<string> tipExplan;
    public List<Image> image;
    public List<Image> character;
    public TMPro.TextMeshProUGUI explanText;

    private int currentFadeIndex = 0;
    private int currentIndex = 0;
    private int currentExplanIndex = 0;
    private float fadeTime = 2.0f;

    private Coroutine animationCoroutine = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Next();
        }
    }

    public void StartTip()
    {
        currentExplanIndex = Random.Range(0, tipExplan.Count);
        explanText.SetText(tipExplan[currentExplanIndex]);
        int randomIndex = Random.Range(0, tipBox.Count);
        image[currentFadeIndex].color = tipBox[randomIndex].color;
        character[currentFadeIndex].sprite = tipBox[randomIndex].sprite;
        character[currentFadeIndex].SetNativeSize();

        currentIndex = randomIndex;
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        animationCoroutine = StartCoroutine(StartTIpAnimation());
    }

    IEnumerator StartTIpAnimation()
    {
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(4.5f);
            yield return null;
            Next();
        }
    }

    public void Next()
    {
        currentFadeIndex = (currentFadeIndex + 1) % 2;
        currentIndex = (currentIndex + 1) % tipBox.Count;
        SetTip(currentIndex);
    }

    public void Prev()
    {
        currentFadeIndex = (currentFadeIndex + 1) % 2;
        currentIndex = (currentIndex - 1 + tipBox.Count) % tipBox.Count;
        SetTip(currentIndex);
    }

    public void SetTip(int index)
    {
        if (tipBox.Count == 0)
            return;

        currentExplanIndex = Random.Range(0, tipExplan.Count);
        explanText.SetText(tipExplan[currentExplanIndex]);

        Color color = tipBox[index].color;
        color.a = 0.0f;
        image[currentFadeIndex].color = color;
        character[currentFadeIndex].sprite = tipBox[index].sprite;
        character[currentFadeIndex].SetNativeSize();

        int nextIndex = (currentFadeIndex + 1) % 2;
        image[currentFadeIndex].DOFade(1.0f, fadeTime);
        character[currentFadeIndex].DOFade(1.0f, fadeTime);

        image[nextIndex].DOFade(0.0f, fadeTime);
        character[nextIndex].DOFade(0.0f, fadeTime);
    }
}