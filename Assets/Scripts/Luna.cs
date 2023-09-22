using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna : Card
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
        GameController controller = GameController.GetInstance();

        if (digit == Digit.Ten)
        {
            controller.CardSwap(controller.ten_cards, controller.one_cards);
        }
    }
}
