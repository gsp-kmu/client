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
        this.transform.name = "Reaper";
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        Transform target = digit == Digit.One ? controller.player_ten.transform : controller.player_one.transform;

        StartCoroutine(controller.OpponentCardSelect(
            card =>
            {
                StartCoroutine(ReaperSkill(card, target));

                Data.DrawCard send_card = new Data.DrawCard();
                send_card.id = "";
                send_card.card.id = "0";
                send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
                send_card.targetId = "1"; // 기본적으로 값은 0, 1이면 상대방
                if (card != null)
                    send_card.targetDigit = card.transform.parent == controller.opponent_one ? Digit.One : Digit.Ten;
                else
                    send_card.targetDigit = Digit.One;

                //NetworkService.Instance.Send(NetworkEvent.INGAME_DRAW_CARD, send_card);
            }));

    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        GameController controller = GameController.GetInstance();

        Card card = target_digit == Digit.One ? controller.player_one_topCard : controller.player_ten_topCard;
        Transform move_target = digit == Digit.One ? controller.opponent_ten.transform : controller.opponent_one.transform;

        StartCoroutine(ReaperSkill(card, move_target));
    }

    static IEnumerator ReaperSkill(Card card, Transform pos)
    {
        if (card == null)
        {
            GameController.GetInstance().turn = !GameController.GetInstance().turn;
            yield break;
        }
        else
        {
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

            GameController.GetInstance().FieldCardOrganize();
        }
    }
}
