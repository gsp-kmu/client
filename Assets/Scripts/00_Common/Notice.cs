using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public void Init(string explan)
    {
        text.SetText(explan);
        gameObject.SetActive(true);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
