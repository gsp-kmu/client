using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleFieldCards : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void OrganizeCard()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i * 2 + 1000;
        }
    }

    public void ReceiveCard(Card card)
    {
        StartCoroutine(MoveCard(card));
    }

    IEnumerator MoveCard(Card card)
    {
        card.transform.parent = transform;
        
        SpriteRenderer sprite = card.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 1000 + transform.childCount * 2;
        card.transform.DOScale(Vector3.one * 2, 0.5f);
        
        while(true)
        {
            card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, Vector3.zero, Time.deltaTime * 5);

            if(Vector3.Distance(card.transform.localPosition, Vector3.zero) < 0.5)
            {
                card.transform.localPosition = Vector3.zero;
                break;
            }       
            yield return new WaitForSeconds(0);
        }
        card.BattleCryOpponent(Card.Digit.One);
    }
}
