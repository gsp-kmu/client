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
        Debug.Log("러브레터발동");

        GameController controller = GameController.GetInstance();
        Card card;
        BattleFieldCards arrive;

        if(digit == Digit.Ten)
        {
            Card[] cards = controller.player_ten.GetComponentsInChildren<Card>();
            card = cards[cards.Length - 1];

            arrive = controller.opponent_ten;
        }
        else
        {
            Card[] cards = controller.player_one.GetComponentsInChildren<Card>();
            card = cards[cards.Length - 1];

            arrive = controller.opponent_one;
        }

        arrive.ReceiveCard(card);
    }
}
