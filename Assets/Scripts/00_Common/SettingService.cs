using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingService : MonoBehaviour
{
    private static SettingService instance = null;
    public static SettingService Instance { get { return instance; } }

    private static string bgm = "SETTING_BGM";
    private static string sfx = "SETTING_SFX";

    private bool isMuteBGM;
    private bool isMuteSFX;

    private void Awake()
    {
        Init();

        if (instance == null)
        {
            isMuteBGM = LoadBool(bgm);
            isMuteSFX = LoadBool(sfx);

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

    private void Init()
    {
        if (PlayerPrefs.HasKey(bgm) == false)
        {
            SetBool(bgm, false);
        }

        if (PlayerPrefs.HasKey(sfx) == false)
        {
            SetBool(sfx, false);
        }
    }

    private void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool LoadBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    public bool GetMuteBGM()
    {
        return isMuteBGM;
    }

    public bool GetMuteSFX()
    {
        return isMuteSFX;
    }

    public void SetMuteBGM(bool isMuteBGM)
    {
        this.isMuteBGM = isMuteBGM;
        SetBool(bgm, isMuteBGM);
        ApplySoundMuteSetting();
    }

    public void SetMuteSFX(bool isMuteSFX)
    {
        this.isMuteSFX = isMuteSFX;
        SetBool(sfx, isMuteSFX);
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
