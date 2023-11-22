using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    public Transform turn1_ts;
    public Transform turn2_ts;

    bool menuTrigger = false;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        TurnAnimation();

        if(menuTrigger && Input.GetMouseButtonUp(0))
            StartCoroutine(ChangeMenuScene());
    }

    public void TurnAnimation()
    {
        if (GameController.GetInstance().myTurn)
        {
            turn1_ts.rotation = Quaternion.Lerp(turn1_ts.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * 45);
            turn2_ts.rotation = Quaternion.Lerp(turn2_ts.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * 45);
        }
        else
        {
            turn1_ts.rotation = Quaternion.Lerp(turn1_ts.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 45);
            turn2_ts.rotation = Quaternion.Lerp(turn2_ts.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 45);
        }
    }

    public void UpdateTurn()
    {
        turn_font.text = GameController.GetInstance().turn.ToString() + "/20";
    }

    public void Surrender()
    {
        NetworkService.Instance.Send(NetworkEvent.INGAME_SURRENDER, "");
    }

    public void MusicButton(bool state)
    {
        music = !music;

        if (music)
            SoundController.GetInstance().background.Play();
        else
            SoundController.GetInstance().background.Stop();

        music_icon.color = music ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }

    public void SoundButton(bool state)
    {
        sound = !sound;

        SoundController.GetInstance().effect_able = sound;

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

    public IEnumerator Result(string what)
    {
        yield return new WaitForSeconds(1f);

        Debug.Log(what);
        game_result.text = what;

        result.SetActive(true);

        Image[] images = result.GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            Color curColor = image.color;
            image.color = new Color(curColor.r, curColor.g, curColor.b, 0);
            image.DOColor(curColor, 1f);
        }

        int score = 0;

        float t = Time.time;

        if (what == "Win")
            SoundController.PlaySound("Win");
        else
            SoundController.PlaySound("Lose");

        while (Time.time - t < 1.5f)
        {
            score = Random.Range(0, 99);
            myScore.text = score.ToString();
            yourScore.text = score.ToString();
            yield return new WaitForSeconds(0);
        }

        score = 0;
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
            score += o_ten_card.num * 10;
        Card o_one_card = GameController.GetInstance().opponent_one_topCard;
        if (o_one_card)
            score += o_one_card.num;

        yourScore.text = score.ToString();

        menuTrigger = true;

    }

    IEnumerator ChangeMenuScene()
    {
        GameObject effect = Instantiate(Resources.Load<GameObject>("Prefebs/SceneTransObject"));
        effect.GetComponent<SceneCardTrans>().FadeIn();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("02_MainScene");
    }
}
