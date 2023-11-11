using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Reaper : Card
{
    public static GameObject reaper_effect = null;

    void Awake()
    {
        if (reaper_effect == null)
            reaper_effect = Resources.Load<GameObject>("Prefebs/Effect/ReaperEffect");
        transform.name = "Reaper";
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        Transform target = digit == Digit.One ? controller.player_ten : controller.player_one;

        StartCoroutine(controller.OpponentCardSelect(
            card =>
            {
                StartCoroutine(ReaperSkill(card, target));

                int targetCard = 0;
                if(card != null)
                    targetCard = card.transform.parent == controller.opponent_one ? 0 : 1;
                SendServerMessage(GameController.GetInstance().playerID, (int)digit, 1, targetCard, 0);
            }));

    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        Card card = target_digit == Digit.One ? controller.player_one_topCard : controller.player_ten_topCard;
        Transform move_target = digit == Digit.One ? controller.opponent_ten.transform : controller.opponent_one.transform;

        StartCoroutine(ReaperSkill(card, move_target));
    }

    static IEnumerator ReaperSkill(Card card, Transform pos)
    {
        if (card == null)
        {
            yield break;
        }
        else
        {
            SoundController.PlaySound("amomo");
           
            card.GetComponent<SpriteRenderer>().sortingOrder = 1500;

            GameObject effect = Instantiate(reaper_effect);
            Destroy(effect, 1.5f);

            effect.transform.position = new Vector3(-50, Random.Range(-10, 10), 0);
            effect.transform.DOMove(card.transform.position, 0.5f);
            yield return new WaitForSeconds(0.5f);

            effect.transform.DOMove(pos.position, 0.5f);
            card.transform.parent = effect.transform;
            yield return new WaitForSeconds(0.5f);

            effect.transform.DOMove(new Vector3(-50, 0, 0), 0.5f);
            card.transform.parent = pos;

            GameController.GetInstance().FieldsCardOrganize();
        }
    }
}
