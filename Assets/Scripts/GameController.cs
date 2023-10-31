using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    static GameController instance = null;
    public static GameController GetInstance()
    {
        return instance;
    }

    public enum Step
    {
        BetFromCardHand,
        SelectPlayerCard,
        SelectOpponentCard,
        SelectAllCard
    }
    public Step curStep;

    public int player_score = 11;
    public int opponent_score = 0;

    public bool ten;

    public List<Card> remove_cards = new List<Card>();

    

    public Card select_card;
    public int select_card_hand_idx;

    public PlayerHand player_hand;
    public BattleFieldCards player_ten;
    public BattleFieldCards player_one;
    public OpponentHand opponent_hand;
    public BattleFieldCards opponent_ten;
    public BattleFieldCards opponent_one;

    public Transform effect_ts;

    public Card player_one_topCard {
        get 
        {
            if (player_one.transform.childCount == 0)
                return null;

            return player_one.transform.GetChild(player_one.transform.childCount - 1).GetComponent<Card>();
        }
    }
    public Card player_ten_topCard
    {
        get 
        {
            if (player_ten.transform.childCount == 0)
                return null;

            return player_ten.transform.GetChild(player_ten.transform.childCount - 1).GetComponent<Card>();
        }
    }

    public Card opponent_one_topCard
    {
        get
        {
            if (opponent_one.transform.childCount == 0)
                return null;

            return opponent_one.transform.GetChild(opponent_one.transform.childCount - 1).GetComponent<Card>();
        }
    }
    public Card opponent_ten_topCard
    {
        get
        {
            if (opponent_ten.transform.childCount == 0)
                return null;


            return opponent_ten.transform.GetChild(opponent_ten.transform.childCount - 1).GetComponent<Card>();
        }
    }
    void Start()
    {
        instance = this;
        curStep = Step.BetFromCardHand;
    }
    
    void Update()
    {
        ControllCard();
        HandCardSort();

        if(Input.GetKeyDown("i"))
        {
            opponent_hand.OpenCard(1, 0);
            opponent_one.ReceiveCard(opponent_hand.cards[1]);
            opponent_hand.UpdateCard();
        }
        if(Input.GetKeyDown("o"))
        {
            opponent_hand.OpenCard(1, 0);
            opponent_ten.ReceiveCard(opponent_hand.cards[1]);
            opponent_hand.UpdateCard();
        }

        if (Input.GetKeyDown("l"))
            StartCoroutine(OpponentCardSelect(card => { Debug.Log(card); }));
        
    }

    void ControllCard()
    {
        if(curStep != Step.BetFromCardHand)
            return;

        Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_point = new Vector3(mouse_point.x, mouse_point.y, 0);

        if(Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouse_point);

            if(!hit)
                return;
            
            PlayerHand is_hand = hit.GetComponentInParent<PlayerHand>();

            if(!is_hand)
                return;
            
            select_card = hit.GetComponent<Card>();

            Card[] cards = player_hand.GetComponentsInChildren<Card>();
            for(int i = 0; i < cards.Length; i++)
            {
                if(cards[i] == select_card)
                    select_card_hand_idx = i;
            }
            select_card.transform.parent = transform;
        }

        if(select_card)
        {
            select_card.transform.position = Vector3.Lerp(select_card.transform.position, mouse_point, Time.deltaTime * 10);
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(select_card == null)
                return;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouse_point);

            foreach(Collider2D hit in hits)
            {
                BattleFieldCards battleField = hit.transform.GetComponent<BattleFieldCards>();
                if(battleField == null)
                    continue;
                if(battleField == player_one)
                    select_card.digit = Card.Digit.One;
                else if(battleField == player_ten)
                    select_card.digit = Card.Digit.Ten;

                StartCoroutine(select_card.PlayCard(battleField.transform));
                select_card.BattleCry(select_card.digit);
                select_card = null;
                break;
            }

            if(select_card)
            {
                select_card.transform.parent = player_hand.transform;
                select_card.transform.SetSiblingIndex(select_card_hand_idx);
                select_card = null;
            }
        }
    }

    void HandCardSort()
    {
        for (int i = 0; i < remove_cards.Count; i++)
        {
            remove_cards[i].transform.position = Vector3.Lerp(remove_cards[i].transform.position, new Vector3(30.0f, 0, 0), Time.deltaTime * 5);
        }
    }

    public void CardSwap(List<Card> depart, List<Card> arrive)
    {
        arrive.Add(depart[depart.Count - 1]);
        depart.RemoveAt(depart.Count - 1);

        arrive[arrive.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 1000 + arrive.Count;
    }

    public IEnumerator OpponentCardSelect(Action<Card> callback)
    {
        yield return new WaitForSeconds(0);

        if (opponent_one.transform.childCount + opponent_ten.transform.childCount == 0)
        {
            Debug.Log("상대방 카드 없음");
            callback(null);
        }
        else
        {
            Card select_card = null;

            Card oneCard = opponent_one_topCard;
            Card tenCard = opponent_ten_topCard;
            oneCard.transform.DOScale(Vector3.one * 2.2f, 0.1f);
            tenCard.transform.DOScale(Vector3.one * 2.2f, 0.1f);

            float oneDelta = UnityEngine.Random.Range(0, Mathf.PI);
            float tenDelta = UnityEngine.Random.Range(0, Mathf.PI);
            while (true)
            {
                oneCard.transform.localPosition = new Vector3(Mathf.Cos(oneDelta) * 0.5f, Mathf.Sin(oneDelta) * 0.5f, 0);
                tenCard.transform.localPosition = new Vector3(Mathf.Cos(tenDelta) * 0.5f, Mathf.Sin(tenDelta) * 0.5f, 0);

                oneDelta += Time.deltaTime * 2;
                tenDelta += Time.deltaTime * 2;

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D hit = Physics2D.OverlapPoint(mouse_point);

                    if (hit == null)
                        continue;

                    BattleFieldCards send_cards = hit.transform.GetComponentInParent<BattleFieldCards>();
                    if (send_cards == null)
                        continue;

                    //print(send_cards.transform.GetChild(send_cards.transform.childCount - 1).GetComponent<Card>());

                    select_card = send_cards.transform.GetChild(send_cards.transform.childCount - 1).GetComponent<Card>();
                    break;
                }
                yield return new WaitForSeconds(0);
            }

            yield return new WaitForSeconds(0.2f);
            callback(select_card);

            oneCard.transform.DOScale(Vector3.one * 2f, 0.1f);
            tenCard.transform.DOScale(Vector3.one * 2f, 0.1f);
            oneCard.transform.localPosition = Vector3.zero;
        }
    }

    public void FieldCardOrganize()
    {
        player_one.OrganizeCard();
        player_ten.OrganizeCard();
        opponent_one.OrganizeCard();
        opponent_ten.OrganizeCard();
    }


}
