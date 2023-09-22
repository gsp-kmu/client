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
        List<Card> depard;
        List<Card> arrive;

        controller.curStep = GameController.Step.SelectOpponentCard;
        if(digit == Digit.One)
            arrive = controller.ten_cards;
        else
            arrive = controller.one_cards;

        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(mouse_point.x > 0 && mouse_point.y > 0)
                {  
                    depard = controller.opponent_one_cards;
                    break;
                }
                else if(mouse_point.x < 0 && mouse_point.y > 0)
                {
                    depard = controller.opponent_ten_cards;
                    break;
                }
            }

            yield return new WaitForSeconds(0.0f);
        }

        controller.CardSwap(depard, arrive);
        
        yield return new WaitForSeconds(0.5f);

        controller.curStep = GameController.Step.BetFromCardHand;

        yield return new WaitForSeconds(0.0f);
    }

}
