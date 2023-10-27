using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Card
{
    public static GameObject reaperEffect = null;

    void Start()
    {
        if(reaperEffect == null)
            reaperEffect = Resources.Load<GameObject>("Prefebs/Effect/ReaperEffect");
    }

    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        StartCoroutine(BattleCrySchedule(digit));
    }

    static IEnumerator ReaperSkill()
    {
        yield return new WaitForSeconds(0);
    }


    static IEnumerator BattleCrySchedule(Digit digit)
    {
        GameController controller = GameController.GetInstance();





        
        BattleFieldCards receive_cards;
        Card select_card = null;

        string givePos = "";
        string receivePos = "";

        if (digit == Digit.One)
        {
            receive_cards = controller.player_ten;
            receivePos = "Ten";
        }
        else
        {
            receive_cards = controller.player_one;
            receivePos = "One";
        }


        if (controller.opponent_one.transform.childCount + controller.opponent_ten.transform.childCount == 2)
        {
            Debug.Log("상대방 필드에 카드가 없음");
        }
        else
        {
            controller.curStep = GameController.Step.SelectOpponentCard;

            while (true)
            {
                yield return new WaitForSeconds(0.0f);

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D hit = Physics2D.OverlapPoint(mouse_point);

                    if (hit == null)
                        continue;

                    BattleFieldCards send_cards = hit.transform.GetComponentInParent<BattleFieldCards>();
                    if (send_cards == null)
                        continue;

                    if (send_cards.transform.name == "OpponentTen")
                        givePos = "Ten";
                    else
                        givePos = "One";

                    select_card = send_cards.transform.GetChild(send_cards.transform.childCount - 1).GetComponent<Card>();

                    break;
                }
            }
            GameObject effect = Instantiate(reaperEffect);
            Animator animator = effect.GetComponent<Animator>();
            animator.Play(givePos + "To" + receivePos);
            Destroy(effect, 2f);

            yield return new WaitForSeconds(1.0f);

            Transform card_pos = effect.transform.Find("CardPos");
            select_card.transform.parent = card_pos;

            yield return new WaitForSeconds(0.5f);

//            receive_cards.ReceiveCard(select_card);

            yield return new WaitForSeconds(0.5f);





            //receive_cards.ReceiveCard(select_card);
            //Debug.Log("사신효과 " + select_card.transform.name + " 이동 " + receive_cards.transform.name);
            //yield return new WaitForSeconds(0.5f);
            //controller.curStep = GameController.Step.BetFromCardHand;
            //yield return new WaitForSeconds(0.0f);
        }
    }


    public override void BattleCryOpponent(Digit digit)
    {
        GameController controller = GameController.GetInstance();
        base.BattleCryOpponent(digit);

        BattleFieldCards receive_cards;
        Digit select_player_digit = Digit.One;

        if(digit == Digit.One)
        {
            receive_cards = controller.opponent_ten;
        }
        else
        {
            receive_cards = controller.opponent_ten;
        }



    }
}
