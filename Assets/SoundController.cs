using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    static SoundController instance;
    public static SoundController GetInstance() { return instance; }

    [SerializeField]
    AudioSource background;
    [SerializeField]
    AudioSource effect;


    void Awake()
    {
        instance = this;
    }

    public static void PlaySound(string s)
    {
        instance.effect.clip = Resources.Load<AudioClip>("Sound/Ingame/" + s);
        instance.effect.Play();
    }
}
