using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public Button LogoutButton;
    public Button UIOnButton;
    public Button CloseButton;

    private void Start() {
        bgmToggle.isOn = SettingService.Instance.GetMuteBGM();
        sfxToggle.isOn = SettingService.Instance.GetMuteSFX();
        bgmToggle.onValueChanged.AddListener(ClickBGMToggle);
        sfxToggle.onValueChanged.AddListener(ClickSFXToggle);
        LogoutButton.onClick.AddListener(Logout);

        UIOnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(true);
        });
        CloseButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void ClickBGMToggle(bool isOn){
        SettingService.Instance.SetMuteBGM(isOn);
    }

    public void ClickSFXToggle(bool isOn){
        SettingService.Instance.SetMuteSFX(isOn);
    }

    public void Logout()
    {
        NetworkService.Instance.io.Disconnect();
    }

}
