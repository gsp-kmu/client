using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : Card
{
    void Start()
    {
        
    }

    void Update()
    {

    }
    public override void BattleCry(Digit digit)
    {
        Debug.Log("�� �ɷ� �ߵ�");
            StartCoroutine(BattleCrySchedule());
    }

    IEnumerator BattleCrySchedule()
    {
        GameController controller = GameController.GetInstance();
        List<Card> depard = null;
        List<Card> arrive = controller.remove_cards;

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mouse_point.x > 0 && mouse_point.y > 0)
                {
                    depard = controller.opponent_one_cards;
                }
                else if(mouse_point.x < 0 && mouse_point.y > 0)
                {
                    depard = controller.opponent_ten_cards;
                }
                else if(mouse_point.x > 0 && mouse_point.y < 0)
                {
                    depard = controller.one_cards;
                }
                else if(mouse_point.x < 0 && mouse_point.y < 0)
                {
                    depard = controller.ten_cards;
                }
                break;
            }
            yield return new WaitForSeconds(0);

        }
        controller.CardSwap(depard, arrive);

        yield return new WaitForSeconds(0.5f);

        controller.curStep = GameController.Step.BetFromCardHand;
    }
}
