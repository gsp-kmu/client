using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sol : Card
{
    void Awake()
    {
        transform.name = "솔";
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

                int target = 0;
                if (card != null)
                    target = card.transform.parent == controller.opponent_one ? 0 : 1;

                SendServerMessage(controller.playerID, (int)digit, 1, target, 0);
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

        Material dissolve = Resources.Load<Material>("Shader/Dissolve");
        card.GetComponent<SpriteRenderer>().material = dissolve;

        dissolve.SetTexture("_MainTexture", card.GetComponent<SpriteRenderer>().sprite.texture);
        dissolve.SetFloat("_DissolveValue", -0.2f);

        float value = -0.2f;
        while (value <= 1.2)
        {
            value += Time.deltaTime * 0.5f;
            dissolve.SetFloat("_DissolveValue", value);
            yield return new WaitForSeconds(0);
        }

        Destroy(card.gameObject);
    }
}
