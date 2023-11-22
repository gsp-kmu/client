using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public AudioClip myTurnSound;
    public TurnUI turnUI;

    public void StartMyTurn(){
        SoundController.PlaySound(myTurnSound);
        turnUI.gameObject.SetActive(true);
        turnUI.StartMyTurnAnimation(GameController.GetInstance().turn);
    }
}
