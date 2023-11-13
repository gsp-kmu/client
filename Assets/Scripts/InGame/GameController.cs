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

    public PlayerHand player_hand;
    public Transform player_ten;
    public Transform player_one;
    public Transform opponent_hand;
    public Transform opponent_ten;
    public Transform opponent_one;

    public Transform effect_ts;

    Card select_card;
    public int select_card_hand_idx;

    public bool myTurn;
    public int turn = 0;

    public bool click_out;
    public Vector3 click_pos;
    public float click_time;

    public int playerID = 0;

    public Card player_one_topCard
    {
        get
        {
            if (player_one.childCount == 0)
                return null;

            return player_one.GetChild(player_one.childCount - 1).GetComponent<Card>();
        }
    }
    public Card player_ten_topCard
    {
        get
        {
            if (player_ten.childCount == 0)
                return null;

            return player_ten.GetChild(player_ten.childCount - 1).GetComponent<Card>();
        }
    }
    public Card opponent_one_topCard
    {
        get
        {
            if (opponent_one.childCount == 0)
                return null;

            return opponent_one.GetChild(opponent_one.childCount - 1).GetComponent<Card>();
        }
    }
    public Card opponent_ten_topCard
    {
        get
        {
            if (opponent_ten.childCount == 0)
                return null;

            return opponent_ten.GetChild(opponent_ten.childCount - 1).GetComponent<Card>();
        }
    }

    void Awake()
    {
        instance = this;
        NetworkService.Instance.Login("A");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Mathcing();

        ControllHandCard();
        ControllOpponentHand();
    }

    void Mathcing()
    {
        Debug.Log("Matching Start");
        NetworkService.Instance.Send(NetworkEvent.MATCH_START, "");
        NetworkService.Instance.AddEvent(NetworkEvent.MATCH_SUCCESS, (string s) =>
        {
            Debug.Log("Matching Success");
        });


        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_INIT_ID, (int id) => {
            Debug.Log("ID : " + id.ToString());
            playerID = id;
        });

        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_TURN, (Data.InGameTurn turn) =>
        {
            myTurn = turn.turn == "1" ? true : false;
            this.turn += 1;
            UIManager.GetInstance().UpdateTurn();
            Debug.Log(turn.turn == "1" ? "내턴" : "상대방 턴");

            if (turn.turn != "1")
            {
                GameObject opponent_card = Instantiate(Resources.Load<GameObject>("Prefebs/OpponentCard"));
                opponent_card.transform.position = new Vector3(0, 100, 0);
                opponent_card.transform.parent = opponent_hand;
            }
        });

        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_FIRST_CARD, (FirstCard cards) =>
        {
            DrawCard(cards.card1.id);
            DrawCard(cards.card2.id);
        });

        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_PLAY_RECV, (Data.RecvPlayCard card) => {
            StartCoroutine(OpponentPlayCard(card.id, card.cardId, card.drawDigit, card.targetId, card.targetDigit, card.targetCardIndex));
            // hyenoseo card.cardIdnex => card.cardId
        });

        NetworkService.Instance.AddEvent(NetworkEvent.INGAME_DRAW_CARD, (Data.Card card) => DrawCard(card.id));
    }

    void ControllHandCard()
    {
        Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_point = new Vector3(mouse_point.x, mouse_point.y, 0);

        //마우스 클릭시 카드를 들기
        if (Input.GetMouseButtonDown(0))
        {
            if (!myTurn)
                return;

            Collider2D hit = Physics2D.OverlapPoint(mouse_point);

            if (!hit)
                return;

            if (player_hand.transform != hit.transform.parent)
                return;

            select_card = hit.GetComponent<Card>();

            DevilIco dev1 = null;
            DevilIco dev2 = null;

            if (player_one_topCard)
                dev1 = player_one_topCard.GetComponent<DevilIco>();
            if (player_ten_topCard)
                dev2 = player_ten_topCard.GetComponent<DevilIco>();


            if ((dev1 != null || dev2 != null) && player_hand.transform.childCount - 1 != select_card.transform.GetSiblingIndex())
            {
                select_card = null;
                return;
            }


            //되돌아갈 인덱스 설정
            select_card_hand_idx = select_card.transform.GetSiblingIndex();

            select_card.transform.parent = transform;
            select_card.GetComponent<SpriteRenderer>().sortingOrder = 10005;

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
            select_card.GetComponent<SpriteRenderer>().sortingOrder = 10000;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouse_point);

            foreach (Collider2D hit in hits)
            {
                if (hit.transform == player_one || hit.transform == player_ten)
                {
                    int cardIndex = select_card.index;
                    player_hand.cards.RemoveAt(cardIndex); // hyeonseo;
                    player_hand.RefreshAllCardIndex(); // hyeonseo;
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

    public void ControllOpponentHand()
    {
        int count = opponent_hand.childCount;

        for (int i = 0; i < opponent_hand.childCount; i++)
        {
            Transform child = opponent_hand.GetChild(i);
            child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, new Vector3(i * 10, 0, 0), Time.deltaTime * 10);
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DrawCard(int card_id)
    {
        Debug.Log("Draw card id : " + card_id.ToString());
        GameObject resources = Resources.Load<GameObject>("Prefebs/Card/" + card_id.ToString());

        if (resources == null)
            resources = Resources.Load<GameObject>("Prefebs/Card/9");
        GameObject card = Instantiate(resources);
        player_hand.cards.Add(card.GetComponent<Card>());
        card.GetComponent<Card>().index = player_hand.cards.Count - 1;
        card.transform.parent = player_hand.transform;
        card.transform.position = new Vector3(0, 100, 0);
        card.transform.localScale = Vector3.one * 4;
        card.transform.DOScale(Vector3.one, 1.0f);
    }

    //적 카드 선택
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
            Card card = null;

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
                    Collider2D[] hits = Physics2D.OverlapPointAll(mouse_point);

                    foreach (Collider2D hit in hits)
                    {
                        if (hit == null)
                            continue;

                        if (hit.transform.parent == opponent_one || hit.transform.parent == opponent_ten)
                        {
                            card = hit.transform.parent.GetChild(hit.transform.parent.childCount - 1).GetComponent<Card>();
                            break;
                        }
                    }
                    if (card)
                        break;
                }
            }

            yield return new WaitForSeconds(0.2f);

            oneCard.transform.localScale = Vector3.one * 2;
            oneCard.transform.localPosition = Vector3.zero;

            tenCard.transform.localScale = Vector3.one * 2;
            tenCard.transform.localPosition = Vector3.zero;

            callback(card);
        }
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
        foreach (Transform field in new List<Transform>() { player_one,
                                                           player_ten,
                                                           opponent_one,
                                                           opponent_ten,})
        {
            FieldCardOrganize(field);
        }
    }

    //적이 카드를 낼 경우
    public IEnumerator OpponentPlayCard(int id, int card_id, int digit, int target, int target_digit, int targetCardIndex) //유저 아이디, 카드정보, 내는숫자, 스킬사용대상(자신, 상대방), 스킬사용대상자릿수
    {
        if (id == playerID)
            yield break;

        Debug.Log("Play card id : " + card_id.ToString() + " target_digit : " + target_digit.ToString() + " targetCardIndex : " + targetCardIndex.ToString());


        int val = UnityEngine.Random.Range(0, opponent_hand.childCount);
        Transform select_card = opponent_hand.GetChild(val);
        select_card.parent = digit == 0 ? opponent_one : opponent_ten;
        select_card.DOLocalMove(Vector3.zero, 0.5f);
        select_card.DOScale(Vector3.one * 2.2f, 0.5f);

        GameObject card = Instantiate(Resources.Load<GameObject>("Prefebs/Card/" + card_id.ToString()));
        card.transform.localScale = Vector3.zero;
        select_card.GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;

        yield return new WaitForSeconds(0.5f);

        select_card.transform.DOScale(Vector3.one * 2.0f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        Destroy(select_card.gameObject);

        card.transform.parent = digit == 0 ? opponent_one : opponent_ten;
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = Vector3.one * 2;
        FieldsCardOrganize();

        card.GetComponent<Card>().BattleCryOpponent((Digit)digit, target, (Digit)target_digit, targetCardIndex);

        yield return new WaitForSeconds(0);
    }
}
