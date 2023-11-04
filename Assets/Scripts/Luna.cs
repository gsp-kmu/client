using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Luna : Card
{
    static GameObject LunaEffect;

    void Awake()
    {
        LunaEffect = Resources.Load<GameObject>("Prefebs/Effect/LunaEffect");
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();
        if (digit == Digit.Ten)
            StartCoroutine(Move(controller.player_ten.transform, controller.player_one.transform));

        Data.DrawCard send_card = new Data.DrawCard();
        send_card.id = "";
        send_card.card.id = "9";
        send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
        send_card.targetId = "0"; // 기본적으로 값은 0, 1이면 상대방
        send_card.targetDigit = digit;
        //NetworkService.Instance.Send(NetworkEvent.INGAME_DRAW_CARD, send_card);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.Ten)
            StartCoroutine(Move(controller.opponent_ten.transform, controller.opponent_one.transform));
    }

    static IEnumerator Move(Transform give, Transform take)
    {
        yield return new WaitForSeconds(0.5f);

        GameObject effect1 = Instantiate(LunaEffect, GameController.GetInstance().effect_ts);
        effect1.transform.position = give.position;
        effect1.transform.localScale = Vector3.zero;
        GameObject effect2 = Instantiate(LunaEffect, GameController.GetInstance().effect_ts);
        effect2.transform.position = take.position;
        effect2.transform.localScale = Vector3.zero;

        effect1.transform.DOScale(Vector3.one * 1.5f, 0.5f);
        effect1.transform.DOPunchRotation(new Vector3(0, 0, 270), 2);
        effect2.transform.DOScale(Vector3.one * 1.5f, 0.5f);
        effect2.transform.DOPunchRotation(new Vector3(0, 0, 270), 2);

        Card[] cards = give.GetComponentsInChildren<Card>();
        Transform card = cards[cards.Length - 1].transform;

        effect1.GetComponent<SpriteRenderer>().sortingOrder = 1499;
        effect2.GetComponent<SpriteRenderer>().sortingOrder = 1499;
        card.GetComponent<SpriteRenderer>().sortingOrder = 1500;

        yield return new WaitForSeconds(0.2f);
        card.DOScale(Vector3.one * 1.2f, 0.1f);
        card.DOPunchRotation(new Vector3(0, 0, 360), 1.4f);

        Camera.main.transform.DOShakePosition(1.4f, 4);
        
        yield return new WaitForSeconds(0.1f);

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
