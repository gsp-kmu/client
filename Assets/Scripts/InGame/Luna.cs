using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Luna : Card
{
    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        SendServerMessage(controller.playerID, (int)digit, GameController.GetInstance().playerID, 0, 0);

        if (digit == Digit.Ten)
            StartCoroutine(Move(controller.player_ten.transform, controller.player_one.transform));

    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.Ten)
            StartCoroutine(Move(controller.opponent_ten.transform, controller.opponent_one.transform));
    }

    static IEnumerator Move(Transform give, Transform take)
    {
        SoundController.PlaySound("아리대사");

        GameController instance = GameController.GetInstance();

        GameObject lunaEffect_go = Resources.Load<GameObject>("Prefebs/Effect/LunaEffect");

        GameObject effect1 = Instantiate(lunaEffect_go, GameController.GetInstance().effect_ts);
        effect1.transform.position = give.position;
        effect1.transform.localScale = Vector3.zero;
        GameObject effect2 = Instantiate(lunaEffect_go, GameController.GetInstance().effect_ts);
        effect2.transform.position = take.position;
        effect2.transform.localScale = Vector3.zero;

        effect1.transform.DOScale(Vector3.one * 1.5f, 0.5f);
        effect1.transform.DOPunchRotation(new Vector3(0, 0, 270), 2);
        effect2.transform.DOScale(Vector3.one * 1.5f, 0.5f);
        effect2.transform.DOPunchRotation(new Vector3(0, 0, 270), 2);

        Card[] cards = give.GetComponentsInChildren<Card>();
        Transform card = cards[cards.Length - 1].transform;
        card.GetComponent<SpriteRenderer>().sortingOrder = 1500;
        card.DOScale(Vector3.one * 1.2f, 0.2f);
        card.DOPunchRotation(new Vector3(0, 0, 360), 1.4f);

        yield return new WaitForSeconds(0.2f);

        card.DOScale(Vector3.zero, 0.6f);

        yield return new WaitForSeconds(0.6f);

        card.parent = take;
        card.localPosition = Vector3.zero;

        card.DOScale(Vector3.one * 2.2f, 0.6f);

        yield return new WaitForSeconds(0.6f);

        card.DOScale(Vector3.one * 2f, 0.1f);

        effect1.transform.DOScale(Vector3.zero, 0.5f);
        effect2.transform.DOScale(Vector3.zero, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(effect1);
        Destroy(effect2);

        GameController.GetInstance().FieldsCardOrganize();
    }
}
