using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card_
{
    public string cardName;
    public Sprite cardImage;
    public int weight;

    public Card_(Card_ card)
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.weight = card.weight;
    }
}
