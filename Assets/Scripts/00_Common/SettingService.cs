using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingService : MonoBehaviour
{
    private static SettingService instance = null;
    public static SettingService Instance { get { return instance; } }

    private bool isMuteBGM;
    private bool isMuteSFX;

    private void Awake()
    {
        if (instance == null)
        {
            isMuteBGM = false;
            isMuteSFX = false;

            instance = this;
            ApplySoundMuteSetting();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            ApplySoundMuteSetting();
            Destroy(this);
        }
    }

    public bool GetMuteBGM(){
        return isMuteBGM;
    }

    public bool GetMuteSFX(){
        return isMuteSFX;
    }

    public void SetMuteBGM(bool isMuteBGM){
        this.isMuteBGM = isMuteBGM;
        ApplySoundMuteSetting();
    }

    public void SetMuteSFX(bool isMuteSFX){
        this.isMuteSFX = isMuteSFX;
        ApplySoundMuteSetting();
    }

    private void ApplySoundMuteSetting()
    {
        AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();
        GameObject[] bgmSources = GameObject.FindGameObjectsWithTag("bgm");

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = SettingService.instance.isMuteSFX;
        }

        foreach (GameObject audioSource in bgmSources)
        {
            audioSource.GetComponent<AudioSource>().mute = SettingService.instance.isMuteBGM; ;
        }

    }
}
