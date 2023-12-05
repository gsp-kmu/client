using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public Button LogoutButton;
    public Button UIOnButton;
    public Button CloseButton;

    public GameObject UI;

    private void Start() {
        bgmToggle.isOn = SettingService.Instance.GetMuteBGM();
        sfxToggle.isOn = SettingService.Instance.GetMuteSFX();
        bgmToggle.onValueChanged.AddListener(ClickBGMToggle);
        sfxToggle.onValueChanged.AddListener(ClickSFXToggle);
        
        if(LogoutButton != null)
            LogoutButton.onClick.AddListener(Logout);

        UIOnButton.onClick.AddListener(() =>
        {
            ButtonClick.instance.PlayButtonClick();
            UI.SetActive(true);
        });
        CloseButton.onClick.AddListener(() =>
        {
            ButtonClick.instance.PlayButtonClick();
            UI.SetActive(false);
        });

        UI.SetActive(false);
    }

    public void ClickBGMToggle(bool isOn)
    {
        ButtonClick.instance.PlayButtonClick();
        SettingService.Instance.SetMuteBGM(isOn);
    }

    public void ClickSFXToggle(bool isOn)
    {
        ButtonClick.instance.PlayButtonClick();
        SettingService.Instance.SetMuteSFX(isOn);
    }

    public void Logout()
    {
        ButtonClick.instance.PlayButtonClick();
        NetworkService.Instance.io.D.Emit("disconnect");
        Destroy(NetworkService.Instance.gameObject);
        SceneManager.LoadScene(GSP.Scene.title);
    }

}
