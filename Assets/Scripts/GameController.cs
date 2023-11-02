using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Data;

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
    public Transform opponent_hand;
    public BattleFieldCards opponent_ten;
    public BattleFieldCards opponent_one;

    public Transform effect_ts;

    public bool turn = true;

    public bool click_out;
    public Vector3 click_pos;
    public float click_time;

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
    void Awake()
    {
        turn = true;

        instance = this;
        curStep = Step.BetFromCardHand;

        //NetworkService.Instance.AddEvent(NetworkEvent.INGAME_PLAY_CARD, (Data.DrawCard card) => {
        //    OpponentPlayCard(card.id, int.Parse(card.card.id), (int)card.drawDigit, int.Parse(card.targetId), (int)card.targetDigit);
        //});
    }

    void Update()
    {
        ControllCard();
        HandCardSort();

        //리퍼 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 0, 0, 1, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 0, 0, 1, 1));
        //if (Input.GetKeyDown(KeyCode.I))
        //    StartCoroutine(OpponentPlayCard("", 0, 1, 1, 0));
        //if (Input.GetKeyDown(KeyCode.U))
        //    StartCoroutine(OpponentPlayCard("", 0, 1, 1, 1));

        //러브레터 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 2, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 2, 1, 0, 0));

        ////솔 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 3, 0, 1, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 3, 0, 1, 1));
        //if (Input.GetKeyDown(KeyCode.I))
        //    StartCoroutine(OpponentPlayCard("", 3, 1, 1, 0));
        //if (Input.GetKeyDown(KeyCode.U))
        //    StartCoroutine(OpponentPlayCard("", 3, 1, 1, 1));

        //타락아이코테스트
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(OpponentPlayCard("", 5, 0, 1, 0));
        if (Input.GetKeyDown(KeyCode.O))
            StartCoroutine(OpponentPlayCard("", 5, 1, 1, 0));

        //타락천사 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 6, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 6, 1, 0, 0));

        //메두사 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 7, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 7, 1, 0, 0));

        //엘프 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 8, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 8, 1, 0, 0));

        //루나 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 9, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 9, 1, 0, 0));
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

            click_out = false;
            click_pos = mouse_point;
            click_time = Time.time;

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
            if (Vector3.Distance(click_pos, mouse_point) > 1)
                click_out = true;

            if (Time.time - click_time > 0.5f && !click_out)
            {
                select_card.transform.position = Vector3.zero;
                select_card.transform.localScale = Vector3.one * 5;
            }
            else
            {
                select_card.transform.position = Vector3.Lerp(select_card.transform.position, mouse_point, Time.deltaTime * 10);
                select_card.transform.localScale = Vector3.one * 1.2f;
            }

        }

        if(Input.GetMouseButtonUp(0))
        {
            if(select_card == null)
                return;

            select_card.transform.localScale = Vector3.one;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouse_point);

            foreach(Collider2D hit in hits)
            {
                BattleFieldCards battleField = hit.transform.GetComponent<BattleFieldCards>();
                if(battleField == null)
                    continue;
                if(battleField == player_one)
                    select_card.digit = Digit.One;
                else if(battleField == player_ten)
                    select_card.digit = Digit.Ten;

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
                tenDelta -= Time.deltaTime * 2;

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

    //적이 카드를 낼 경우
    public IEnumerator OpponentPlayCard(string id, int card_id, int digit, int target, int target_digit) //유저 아이디, 카드정보, 내는숫자, 스킬사용대상(자신, 상대방), 스킬사용대상자릿수
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Prefebs/Card/" + card_id.ToString()));
        card.transform.parent = opponent_hand;
        card.transform.localPosition = Vector3.zero;

        card.transform.parent = digit == 0 ? opponent_one.transform : opponent_ten.transform;

        card.transform.DOLocalMove(Vector3.zero, 0.5f);
        card.transform.DOScale(Vector3.one * 2.2f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        card.transform.DOScale(Vector3.one * 2.0f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        FieldCardOrganize();

        card.GetComponent<Card>().BattleCryOpponent((Digit)digit, target, (Digit)target_digit);

        yield return new WaitForSeconds(0);
    }

    public void FieldCardOrganize()
    {
        player_one.OrganizeCard();
        player_ten.OrganizeCard();
        opponent_one.OrganizeCard();
        opponent_ten.OrganizeCard();
    }


}
