using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public AudioClip myTurnSound;
    public TurnUI turnUI;
    public TurnUI opponentUI;

    public void StartMyTurn(){
        SoundController.PlaySound(myTurnSound);
        turnUI.gameObject.SetActive(true);
        turnUI.StartMyTurnAnimation(GameController.GetInstance().turn);
    }

    public void StartOpponentTurn()
    {
        SoundController.PlaySound(myTurnSound);
        opponentUI.gameObject.SetActive(true);
        opponentUI.StartMyTurnAnimation(GameController.GetInstance().turn);

    }
}
