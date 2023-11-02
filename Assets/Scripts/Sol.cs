using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sol : Card
{
    static GameObject sol_effect = null;

    void Awake()
    {
        if (sol_effect == null)
            sol_effect = Resources.Load<GameObject>("Prefebs/Effect/SolEffect");
        transform.name = "Sol";
    }


    void Update()
    {

    }
    public override void BattleCry(Digit digit)
    {

        GameController controller = GameController.GetInstance();
        StartCoroutine(controller.OpponentCardSelect(
            card => {
                StartCoroutine(SolSkill(card));

                Data.PlayCard send_card = new Data.PlayCard();
                send_card.id = "";
                send_card.card.id = "0";
                send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
                send_card.targetId = "1"; // 기본적으로 값은 0, 1이면 상대방
                if (card != null)
                    send_card.targetDigit = card.transform.parent == controller.opponent_one ? Digit.One : Digit.Ten;
                else
                    send_card.targetDigit = Digit.One;
            }));

    }
    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        GameController controller = GameController.GetInstance();

        Card card = target_digit == Digit.One ? controller.player_one_topCard : controller.player_ten_topCard;

        StartCoroutine(SolSkill(card));
    }

    public IEnumerator SolSkill(Card card)
    {
        if (card == null)
            yield break;

        GameObject effect = Instantiate(sol_effect, GameController.GetInstance().effect_ts);
        effect.transform.position = card.transform.position + new Vector3(0, 0, -5);

        effect.GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;
        Material material = effect.GetComponent<SpriteRenderer>().material;
        float value = 0;

        Destroy(effect, 2f);
        Destroy(card.gameObject);

        while (value < 1)
        {
            material.SetFloat("_SliceAmount", value);
            value += Time.deltaTime * 0.5f;
            yield return new WaitForSeconds(0);
        }
    }
}
