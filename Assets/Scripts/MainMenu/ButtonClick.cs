using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public static ButtonClick instance;
    public AudioSource audioSource;
    private void Start()
    {
        instance = this;
    }

    public void PlayButtonClick()
    {
        audioSource.Play();
    }
}
