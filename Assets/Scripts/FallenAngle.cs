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
        Debug.Log("Ÿ��õ�� �ɷ� �ߵ�");
        StartCoroutine(BattleCrySchedule());
    }
    IEnumerator BattleCrySchedule()
    {
        GameController controller = GameController.GetInstance();
        Card cardOne;
        Card cardTen;
        List<Card> one_cards = controller.opponent_one_cards;
        List<Card> ten_cards = controller.opponent_ten_cards;

        cardOne = one_cards[one_cards.Count - 1];
        cardTen = ten_cards[ten_cards.Count - 1];
            
        one_cards.RemoveAt(one_cards.Count - 1);
        ten_cards.RemoveAt(ten_cards.Count - 1);

        one_cards.Add(cardTen);
        ten_cards.Add(cardOne);

        one_cards[one_cards.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 1000 + one_cards.Count;
        ten_cards[ten_cards.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 1000 + ten_cards.Count;

        yield return new WaitForSeconds(0.5f);

        controller.curStep = GameController.Step.BetFromCardHand;
    }

}
