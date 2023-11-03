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

        //NetworkService.Instance.AddEvent(NetworkEvent.INGAME_PLAY_CARD, (Data.DrawCard card) => {
        //    OpponentPlayCard(card.id, int.Parse(card.card.id), (int)card.drawDigit, int.Parse(card.targetId), (int)card.targetDigit);
        //});
    }

    void Update()
    {
        ControllHandCard();
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
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 5, 0, 1, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 5, 1, 1, 0));

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
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(OpponentPlayCard("", 8, 0, 0, 0));
        if (Input.GetKeyDown(KeyCode.O))
            StartCoroutine(OpponentPlayCard("", 8, 1, 0, 0));

        //루나 테스트
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartCoroutine(OpponentPlayCard("", 9, 0, 0, 0));
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(OpponentPlayCard("", 9, 1, 0, 0));
    }
 
    void ControllHandCard()
    {
        Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_point = new Vector3(mouse_point.x, mouse_point.y, 0);

        //마우스 클릭시 카드를 들기
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouse_point);

            if(!hit)
                return;

            if (player_hand.transform != hit.transform.parent)
                return;
            
            select_card = hit.GetComponent<Card>();

            //되돌아갈 인덱스 설정
            select_card_hand_idx = select_card.transform.GetSiblingIndex();

            select_card.transform.parent = transform;

            //select_card 선택 타이밍 위치 저장 (카드 확대 하기 위함)
            click_out = false;
            click_pos = mouse_point;
            click_time = Time.time;
        }
        //카드를 내기
        if (Input.GetMouseButtonUp(0))
        {
            if (select_card == null)
                return;

            select_card.transform.localScale = Vector3.one;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouse_point);

            foreach (Collider2D hit in hits)
            {
                if(hit.transform == player_one.transform || hit.transform == player_ten.transform)
                {
                    StartCoroutine(select_card.PlayCard(hit.transform));
                    select_card.BattleCry(hit.transform == player_one ? Digit.One : Digit.Ten);
                    select_card = null;
                    break;
                }
            }

            //만약 내지 못했을 경우 손패로 다시 돌아가기
            if (select_card)
            {
                select_card.transform.parent = player_hand.transform;
                select_card.transform.SetSiblingIndex(select_card_hand_idx);
                select_card = null;
            }
        }
        //카드를 들고 있을경우
        if (select_card)
        {
            if (Vector3.Distance(click_pos, mouse_point) > 1)
                click_out = true;

            if (Time.time - click_time > 0.5f && !click_out) //카드 확대
            { 
                select_card.transform.position = Vector3.zero;
                select_card.transform.localScale = Vector3.one * 5;
            }
            else //카드 마우스 따라 다니기
            {
                select_card.transform.position = Vector3.Lerp(select_card.transform.position, mouse_point, Time.deltaTime * 10);
                select_card.transform.localScale = Vector3.one * 1.2f;
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

    public IEnumerator OpponentCardSelect(Action<Card> callback)
    {
        Card oneCard = opponent_one_topCard;
        Card tenCard = opponent_ten_topCard;

        if (oneCard == null && tenCard == null)
        {
            Debug.Log("상대방 카드 없음");
            callback(null);
        }
        else if (oneCard == null)
        {
            callback(tenCard);
        }
        else if (tenCard == null)
        {
            callback(oneCard);
        }
        else
        {
            oneCard.transform.DOScale(Vector3.one * 2.2f, 0.1f);
            tenCard.transform.DOScale(Vector3.one * 2.2f, 0.1f);

            float oneDelta = UnityEngine.Random.Range(0, Mathf.PI);
            float tenDelta = UnityEngine.Random.Range(0, Mathf.PI);
            while (true)
            {
                yield return new WaitForSeconds(0);

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

                    if(hit.transform.parent == opponent_one && hit.transform.parent == opponent_ten)
                        select_card = hit.transform.parent.GetChild(hit.transform.childCount - 1).GetComponent<Card>();
                    if (select_card == null)
                        continue;

                    break;
                }
            }

            yield return new WaitForSeconds(0.2f);

            oneCard.transform.localScale = Vector3.one * 2;
            oneCard.transform.localPosition = Vector3.zero;

            tenCard.transform.localScale = Vector3.one * 2;
            tenCard.transform.localPosition = Vector3.zero;

            callback(select_card);
            select_card = null;
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

        FieldsCardOrganize();

        card.GetComponent<Card>().BattleCryOpponent((Digit)digit, target, (Digit)target_digit);

        yield return new WaitForSeconds(0);
    }

    //선택한 필드에 카드들 Sprite 순서 정렬
    public void FieldCardOrganize(Transform field)
    {
        for (int i = 0; i < field.childCount; i++)
            field.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i * 2 + 1000;
    }

    //모든 필드 카드 순서 정렬
    public void FieldsCardOrganize()
    {

        foreach(Transform field in new List<Transform>() { player_one.transform,
                                                        player_ten.transform,
                                                        opponent_one.transform,
                                                        opponent_ten.transform,})
        {
            FieldCardOrganize(field);
        }
    }
}
