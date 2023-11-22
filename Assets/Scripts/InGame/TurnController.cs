using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public AudioClip myTurnSound;

    public void StartMyTurn(){
        SoundController.PlaySound(myTurnSound);
    }
}
