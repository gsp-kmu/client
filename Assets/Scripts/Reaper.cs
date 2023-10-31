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
        if(reaper_effect == null)
            reaper_effect = Resources.Load<GameObject>("Prefebs/Effect/ReaperEffect");
    }

    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        Transform target = digit == Digit.One ? controller.player_ten.transform : controller.player_one.transform;

        StartCoroutine(controller.OpponentCardSelect(
            card => { 
                StartCoroutine(ReaperSkill(card, target)); 
            }));

    }

    public override void BattleCryOpponent(Digit digit)
    {
        base.BattleCryOpponent(digit);

        string order = "One";

        GameController controller = GameController.GetInstance();

        Card card = order == "One" ? controller.player_one_topCard : controller.player_ten_topCard;

        Transform target = digit == Digit.One ? controller.opponent_ten.transform : controller.opponent_one.transform;

        StartCoroutine(ReaperSkill(card, target));
    }

    static IEnumerator ReaperSkill(Card card, Transform pos)
    {
        if (card == null)
            yield break;

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
