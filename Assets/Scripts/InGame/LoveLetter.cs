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

        SendServerMessage(GameController.GetInstance().playerID, (int)digit, 0, 0, 0);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.One)
            StartCoroutine(LoveLetterSkill(controller.opponent_one_topCard, controller.player_one.transform));
        else
            StartCoroutine(LoveLetterSkill(controller.opponent_ten_topCard, controller.player_ten.transform));
    }

    public IEnumerator LoveLetterSkill(Card card, Transform target)
    {
        //SoundController.PlaySound("timo");
        yield return new WaitForSeconds(0.5f);
        card.transform.DOMove(target.position, 0.5f);

        card.GetComponent<SpriteRenderer>().sortingOrder = 1500;

        yield return new WaitForSeconds(0.5f);

        GameObject effect = Instantiate(loveletter_effect, GameController.GetInstance().effect_ts);
        card.transform.parent = target;
        GameController.GetInstance().FieldsCardOrganize();

        effect.transform.position = target.position;
        effect.transform.localScale = new Vector3(0, 0, 0);
        effect.transform.DOScale(new Vector3(20, 20, 20), 0.7f);
        effect.GetComponent<SpriteRenderer>().DOFade(0, 0.7f);

        yield return new WaitForSeconds(0.7f);

        Destroy(effect);


    }
}
