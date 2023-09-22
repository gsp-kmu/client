using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : Card
{
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        Debug.Log("해커 능력 발동");
        StartCoroutine(BattleCrySchedule(digit));
    }

    static IEnumerator BattleCrySchedule(Digit digit)
    {
        GameController controller = GameController.GetInstance();
        Card side_card;

        if(digit == Digit.One)
            side_card = controller.ten_cards[controller.ten_cards.Count - 1];
        else
            side_card = controller.one_cards[controller.one_cards.Count - 1];

        side_card.BattleCry(digit);

        while(true)
        {
            if(controller.curStep == GameController.Step.BetFromCardHand)
                break;
            yield return new WaitForSeconds(0.0f);
        }
    }
}
