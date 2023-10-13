using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FallenAngle : Card
    {
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BattleCry( Digit digit)
    {
        base.BattleCry(digit);

        StartCoroutine(BattleCrySchedule());
    }
    IEnumerator BattleCrySchedule()
    {
        GameController controller = GameController.GetInstance();

        Card cardOne = null;
        Card cardTen = null;

        Card[] cards = controller.opponent_one.GetComponentsInChildren<Card>();

        if (cards.Length > 0)
            cardOne = cards[cards.Length - 1];

        cards = controller.opponent_ten.GetComponentsInChildren<Card>();

        if (cards.Length > 0)
            cardTen = cards[cards.Length - 1];

        if (cardOne != null && cardTen != null)
        {
            controller.opponent_one.ReceiveCard(cardTen);
            controller.opponent_ten.ReceiveCard(cardOne);
        }

        yield return new WaitForSeconds(0.5f);

        controller.curStep = GameController.Step.BetFromCardHand;
    }

}
