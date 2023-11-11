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
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();
        Card side_card = digit == Digit.One ? controller.player_ten_topCard : controller.player_one_topCard;

        Card.deputy = this;
        side_card.BattleCry(digit);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        Card side_card = digit == Digit.One ? controller.opponent_ten_topCard : controller.opponent_one_topCard;
        Digit side_digit = digit == Digit.One ? Digit.Ten : Digit.One;

        side_card.BattleCryOpponent(side_digit, target, target_digit, targetCardIndex);
    }
}
