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
                
                battleField.ReceiveCard(select_card);
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


}
