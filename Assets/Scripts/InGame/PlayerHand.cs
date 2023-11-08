using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();// hyeonseo;
    void Update()
    {
        MoveCard();
    }

    public void RefreshAllCardIndex()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].index = i;
        }
    }

    void MoveCard()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).transform.localPosition, new Vector3(i * 8.0f - transform.childCount * 8.0f * 0.5f + 4.0f, 0, 0), Time.deltaTime * 5);
        }
    }
}
