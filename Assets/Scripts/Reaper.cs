using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Card
{
    void Start()
    {
    }

    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        Debug.Log("사신 능력 발동");
        StartCoroutine(BattleCrySchedule(digit));
    }

    static IEnumerator BattleCrySchedule(Digit digit)
    {
        GameController controller = GameController.GetInstance();
        Card select_card = null;
        BattleFieldCards receive_cards;

        controller.curStep = GameController.Step.SelectOpponentCard;

        if(digit == Digit.One)
            receive_cards = controller.player_ten;
        else
            receive_cards = controller.player_one;

        while(true)
        {
            yield return new WaitForSeconds(0.0f);
            if(Input.GetMouseButtonDown(0))
            {   
                Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = Physics2D.OverlapPoint(mouse_point);

                Debug.Log(hit);
                if(hit == null)
                    continue;
                
                BattleFieldCards send_cards = hit.transform.GetComponentInParent<BattleFieldCards>();
                if(send_cards == null)
                    continue;

                select_card = send_cards.transform.GetChild(send_cards.transform.childCount - 1).GetComponent<Card>();
                break;
            }
        }

        receive_cards.ReceiveCard(select_card);

        yield return new WaitForSeconds(0.5f);

        controller.curStep = GameController.Step.BetFromCardHand;

        yield return new WaitForSeconds(0.0f);
    }

}
