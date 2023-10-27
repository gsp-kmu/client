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

    public void ReceiveCard(Card card)
    {
        StartCoroutine(MoveCard(card));
    }

    IEnumerator MoveCard(Card card)
    {
        card.transform.parent = transform;
        
        SpriteRenderer sprite = card.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 1000 + transform.childCount;
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
    }
}
