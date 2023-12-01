using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    static SoundController instance;
    public static SoundController GetInstance() { return instance; }

    [SerializeField]
    public AudioSource background;
    [SerializeField]
    public AudioSource effect;
    [SerializeField]
    public AudioSource environment;

    public bool effect_able = true;


    void Awake()
    {
        instance = this;
    }

    public static void PlaySound(AudioClip clip){
        instance.effect.PlayOneShot(clip);
    }

    public static void PlaySound(string s)
    {
        if (!instance.effect_able)
            return;

        instance.effect.clip = Resources.Load<AudioClip>("Sound/Ingame/" + s);
        instance.effect.Play();
    }

    public static void PlayEnvironment(string s)
    {
        if (!instance.effect_able)
            return;

        instance.environment.clip = Resources.Load<AudioClip>("Sound/" + s);
        instance.environment.Play();
    }
}
