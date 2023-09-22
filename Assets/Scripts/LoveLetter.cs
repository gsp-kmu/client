using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveLetter : Card
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
        Debug.Log("러브레터");

        GameController controller = GameController.GetInstance();
        List<Card> depard;
        List<Card> arrive;

        if(digit == Digit.Ten)
        {
            depard = controller.ten_cards;
            arrive = controller.opponent_ten_cards;
        }
        else
        {
            depard = controller.one_cards;
            arrive = controller.opponent_one_cards;
        }

        controller.CardSwap(depard, arrive);
    }
}
