using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Hacker : Card
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.name = "해커";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();
        Card side_card = digit == Digit.One ? controller.player_ten_topCard : controller.player_one_topCard;

        Card.deputy = this;


        if (side_card != null)
        {

            Hacker h = side_card.GetComponent<Hacker>();
            if (h == null)
            {
                side_card.BattleCry(digit);
            }
            else
            {
                SendServerMessage(controller.playerID, (int)digit, 0, 0, 0);
            }

        }
        else
            SendServerMessage(controller.playerID, (int)digit, 0, 0, 0);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        Card side_card = digit == Digit.One ? controller.opponent_ten_topCard : controller.opponent_one_topCard;
        Digit side_digit = digit == Digit.One ? Digit.Ten : Digit.One;

        if (side_card != null)
        {
            Hacker h = side_card.GetComponent<Hacker>();
            if (h == null)
                side_card.BattleCryOpponent(digit, target, target_digit, targetCardIndex);
        }
    }
}
