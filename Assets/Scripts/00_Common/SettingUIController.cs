using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private void Start() {
        bgmToggle.isOn = SettingService.Instance.GetMuteBGM();
        sfxToggle.isOn = SettingService.Instance.GetMuteSFX();
        bgmToggle.onValueChanged.AddListener(ClickBGMToggle);
        sfxToggle.onValueChanged.AddListener(ClickSFXToggle);
    }

    public void ClickBGMToggle(bool isOn){
        SettingService.Instance.SetMuteBGM(isOn);
    }

    public void ClickSFXToggle(bool isOn){
        SettingService.Instance.SetMuteSFX(isOn);
    }

}
