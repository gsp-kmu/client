using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager GetInstance() { return instance; }

    public TextMeshProUGUI turn_font;

    public bool pause = false;
    public GameObject pause_ui;

    public bool music = true;
    public Image music_icon;

    public bool sound = true;
    public Image sound_icon;

    public bool vibration = true;
    public Image vibration_icon;

    public GameObject result;
    public TextMeshProUGUI game_result;
    public TextMeshProUGUI myScore;
    public TextMeshProUGUI yourScore;

    public void Awake()
    {
        instance = this;
        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_END_WIN, (Data.InGameEnd winId) =>
        {
            instance.Win();
        });
        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_END_LOSE, (Data.InGameEnd winId) =>
        {
            instance.Lose();
        });

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

    public void Win()
    {
        result.SetActive(true);
        game_result.text = "Win";

        int score = 0;
        Card ten_card = GameController.GetInstance().player_ten_topCard;
        if (ten_card)
            score += ten_card.num * 10;
        Card one_card = GameController.GetInstance().player_one_topCard;
        if (one_card)
            score += one_card.num;

        myScore.text = score.ToString();

        score = 0;
        Card o_ten_card = GameController.GetInstance().opponent_ten_topCard;
        if (o_ten_card)
            score += ten_card.num * 10;
        Card o_one_card = GameController.GetInstance().opponent_one_topCard;
        if (o_one_card)
            score += one_card.num;

        yourScore.text = score.ToString();

    }

    public void Lose()
    {
        result.SetActive(true);
        game_result.text = "Loss";

        int score = 0;
        Card ten_card = GameController.GetInstance().player_ten_topCard;
        if (ten_card)
            score += ten_card.num * 10;
        Card one_card = GameController.GetInstance().player_one_topCard;
        if (one_card)
            score += one_card.num;

        myScore.text = score.ToString();

        score = 0;
        Card o_ten_card = GameController.GetInstance().opponent_ten_topCard;
        if (o_ten_card)
            score += ten_card.num * 10;
        Card o_one_card = GameController.GetInstance().opponent_one_topCard;
        if (o_one_card)
            score += one_card.num;

        yourScore.text = score.ToString();
    }
}
