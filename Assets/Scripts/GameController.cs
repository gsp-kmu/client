using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool turn;

    public bool ten;

    public List<Card> hand_cards = new List<Card>();
    public List<Card> one_cards = new List<Card>();
    public List<Card> ten_cards = new List<Card>();

    public List<Card> opponent_hand_cards = new List<Card>();
    public List<Card> opponent_one_cards = new List<Card>();
    public List<Card> opponent_ten_cards = new List<Card>();

    public List<Card> remove_cards = new List<Card>();

    

    public Card select_card;

    public OpponentHand opponent_hand;
    public BattleFieldCards opponent_ten;

    public BattleFieldCards opponent_one;

    void Start()
    {
        instance = this;
        curStep = Step.BetFromCardHand;
    }
    
    void Update()
    {
        ControllCard();
        HandCardSort();
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

            if(hit)
            {
                select_card = hit.transform.GetComponent<Card>();

                if(!hand_cards.Contains(select_card))
                    select_card = null;
            }
        }

        if(select_card)
        {
            select_card.transform.position = Vector3.Lerp(select_card.transform.position, mouse_point, Time.deltaTime * 10);
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(select_card == null)
                return;

            if(mouse_point.y > -40.0 && mouse_point.y < 0)
            {
                if(mouse_point.x < 0)
                {
                    select_card.digit = Card.Digit.Ten;
                    ten_cards.Add(select_card);

                    select_card.GetComponent<SpriteRenderer>().sortingOrder = 1000 + ten_cards.Count; 
                }
                else
                {
                    select_card.digit = Card.Digit.One;
                    one_cards.Add(select_card);

                    select_card.GetComponent<SpriteRenderer>().sortingOrder = 1000 + one_cards.Count;
                }

                hand_cards.Remove(select_card);
            }

            select_card.BattleCry(select_card.digit);


            select_card = null;

        }
    }

    void HandCardSort()
    {
        for(int i = 0; i < hand_cards.Count; i++)
        {
            if(hand_cards[i] == select_card)
                continue;

            hand_cards[i].transform.position = Vector3.Lerp(hand_cards[i].transform.position, new Vector3(i * 8.0f - hand_cards.Count * 8.0f * 0.5f + 4.0f, -40.0f, 0),Time.deltaTime * 5);
        }

        for(int i = 0; i < one_cards.Count; i++)
        {
            one_cards[i].transform.position = Vector3.Lerp(one_cards[i].transform.position, new Vector3(10.0f, -15.0f, 0),Time.deltaTime * 5);
        }


        for(int i = 0; i < ten_cards.Count; i++)
        {
            ten_cards[i].transform.position = Vector3.Lerp(ten_cards[i].transform.position, new Vector3(-10.0f, -15.0f, 0),Time.deltaTime * 5);
        }

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


}
