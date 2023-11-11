using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIManager instance;
    public UIManager GetInstance() { return instance; }

    public Text turn_font;

    public bool pause = false;
    public GameObject pause_ui;

    public bool music = true;
    public Image music_icon;

    public bool sound = true;
    public Image sound_icon;

    public bool vibration = true;
    public Image vibration_icon;

    public void Awake()
    {
        instance = this;
    }

    public void UpdateTurn()
    {
        turn_font.text = GameController.GetInstance().turn.ToString() + "/20";
    }

    public void Surrender()
    {

    }

    public void MusicButton(bool state)
    {
        music = !music;

        music_icon.color = music ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }

    public void SoundButton(bool state)
    {
        sound = !sound;

        sound_icon.color = sound ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }

    public void Vibration(bool state)
    {
        vibration = !vibration;

        vibration_icon.color = vibration ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }

    public void OnPause()
    {
        pause = true;
        pause_ui.SetActive(pause);
    }

    public void OffPause()
    {
        pause = false;
        pause_ui.SetActive(pause);
    }
}
