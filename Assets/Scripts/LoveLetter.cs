using Data;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LoveLetter : Card
{
    GameObject loveletter_effect = null;

    void Awake()
    {
        if (loveletter_effect == null)
            loveletter_effect = Resources.Load<GameObject>("Prefebs/Effect/LoveLetter_effect");
        transform.name = "LoveLetter";
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.One)
            StartCoroutine(LoveLetterSkill(controller.player_one_topCard, controller.opponent_one.transform));
        else
            StartCoroutine(LoveLetterSkill(controller.player_ten_topCard, controller.opponent_ten.transform));

        Data.DrawCard send_card = new Data.DrawCard();
        send_card.id = "";
        send_card.card.id = "2";
        send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
        send_card.targetId = "0"; // 기본적으로 값은 0, 1이면 상대방
        send_card.targetDigit = digit;
        //NetworkService.Instance.Send(NetworkEvent.INGAME_DRAW_CARD, send_card);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.One)
            StartCoroutine(LoveLetterSkill(controller.opponent_one_topCard, controller.player_one.transform));
        else
            StartCoroutine(LoveLetterSkill(controller.opponent_ten_topCard, controller.player_ten.transform));
    }

    public IEnumerator LoveLetterSkill(Card card, Transform target)
    {
        yield return new WaitForSeconds(0.5f);
        card.transform.DOMove(target.position, 0.5f);

        card.GetComponent<SpriteRenderer>().sortingOrder = 1500;

        yield return new WaitForSeconds(0.5f);

        GameObject effect = Instantiate(loveletter_effect, GameController.GetInstance().effect_ts);
        effect.transform.position = target.position;
        effect.transform.localScale = new Vector3(0, 0, 0);
        effect.transform.DOScale(new Vector3(20, 20, 20), 0.7f);
        effect.GetComponent<SpriteRenderer>().DOFade(0, 0.7f);

        yield return new WaitForSeconds(0.7f);

        card.transform.parent = target;
        GameController.GetInstance().FieldsCardOrganize();
        Destroy(effect);


    }
}
