using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHand : MonoBehaviour
{
    public Card[] cards;
    public Sprite tex;

    void Start()
    {
        UpdateCard();
    }

    void Update()
    {
        MoveCard();

        if(Input.GetKeyDown("u"))
            StartCoroutine(OpenCard(1, 1));
    }

    public void MoveCard()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.localPosition = Vector3.Lerp(cards[i].transform.localPosition, new Vector3(i * 8.0f - cards.Length * 8.0f * 0.5f + 4.0f, 0, 0), Time.deltaTime * 5);
        }
    }

    public void UpdateCard()
    {
        cards = transform.GetComponentsInChildren<Card>();
    }

    public IEnumerator OpenCard(int card_idx, int card_cord)
    {
        Card card = cards[card_idx];
        
        while(true)
        {
            card.transform.rotation = Quaternion.RotateTowards(card.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 90 * 3);

            if(card.transform.rotation == Quaternion.Euler(0, 90, 0))
                break;
                
            yield return new WaitForSeconds(0);
        }

        SpriteRenderer sprite = card.transform.GetComponent<SpriteRenderer>();
        sprite.sprite = tex;

        while(true)
        {
            card.transform.rotation = Quaternion.RotateTowards(card.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 90 * 3);

            if(card.transform.rotation == Quaternion.Euler(0, 0, 0))
                break;
            yield return new WaitForSeconds(0);
        }
    }
}
