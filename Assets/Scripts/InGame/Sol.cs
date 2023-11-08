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

                int target = card.transform.parent == controller.opponent_one ? 0 : 1;

                SendServerMessage(controller.playerID, 4, (int)digit, 1, target, 0);
            }));

    }
    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

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
