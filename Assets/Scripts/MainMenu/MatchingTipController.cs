using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TipBox
{
    public Sprite sprite;
    public string explan;
}

public class MatchingTipController : MonoBehaviour
{
    public List<TipBox> tipBox;
    public Image image;
    public TMPro.TextMeshProUGUI text;

    private int currentIndex = 0;

    public void StartTip()
    {
        currentIndex = 0;
        SetTip(currentIndex);
    }

    public void Next()
    {
        currentIndex = Mathf.Min(currentIndex + 1, tipBox.Count - 1);
        SetTip(currentIndex);
    }

    public void Prev()
    {
        currentIndex = Mathf.Max(currentIndex - 1, 0);
        SetTip(currentIndex);
    }

    public void SetTip(int index)
    {
        if (tipBox.Count == 0)
            return;

        image.sprite = tipBox[index].sprite;
        text.SetText(tipBox[index].explan);
    }
}