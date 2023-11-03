using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.U2D.Aseprite;

public class FallenAngle : Card
{
    GameObject ghost;

    void Awake()
    {
        if (ghost == null)
            ghost = Resources.Load<GameObject>("Prefebs/Effect/ghost");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        StartCoroutine(FallenAngelSkill(GameController.GetInstance().opponent_ten_topCard, GameController.GetInstance().opponent_one_topCard));

        Data.DrawCard send_card = new Data.DrawCard();
        send_card.id = "";
        send_card.card.id = "6";
        send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
        send_card.targetId = "0"; // 기본적으로 값은 0, 1이면 상대방
        send_card.targetDigit = digit;
        //NetworkService.Instance.Send(NetworkEvent.INGAME_DRAW_CARD, send_card);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        StartCoroutine(FallenAngelSkill(GameController.GetInstance().player_one_topCard, GameController.GetInstance().player_ten_topCard));
    }

    IEnumerator FallenAngelSkill(Card card1, Card card2)
    {
        if (card1 == null || card2 == null)
        {
            yield break;
        }

        GameController controller = GameController.GetInstance();

        GameObject ghost1 = Instantiate(ghost, controller.effect_ts);
        GameObject ghost2 = Instantiate(ghost, controller.effect_ts);

        ghost1.transform.position = card1.transform.position;
        ghost2.transform.position = card2.transform.position;
        ghost2.transform.rotation = Quaternion.Euler(0, 0, 180);

        SpriteRenderer ghost1_sprite = ghost1.GetComponent<SpriteRenderer>();
        SpriteRenderer ghost2_sprite = ghost2.GetComponent<SpriteRenderer>();
        ghost1_sprite.color = new Color(1, 1, 1, 0);
        ghost2_sprite.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0);

        card1.transform.DOScale(0, 0.5f);
        card2.transform.DOScale(0, 0.5f);

        ghost1_sprite.DOFade(1, 0.5f);
        ghost2_sprite.DOFade(1, 0.5f);

        yield return new WaitForSeconds(0.3f);

        Vector3 ghost1_target = ghost2.transform.position;
        Vector3 ghost2_target = ghost1.transform.position;
        ghost1.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 180) * ghost1.transform.rotation, 0.25f);
        ghost2.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 180) * ghost2.transform.rotation, 0.25f);

        Vector3 center = (ghost1_target + ghost2_target) * 0.5f;
        Debug.Log(center);

        while (Vector3.Distance(ghost1.transform.position, ghost1_target) > 1)
        {
            ghost1.transform.position = Vector3.RotateTowards(ghost1.transform.position, ghost1_target, Time.deltaTime * 3, 0);
            ghost2.transform.position = center + (center - ghost1.transform.position);

            yield return new WaitForSeconds(0);
        }

        Transform ts = card1.transform.parent;
        card1.transform.parent = card2.transform.parent;
        card2.transform.parent = ts;

        card1.transform.localPosition = Vector3.zero;
        card2.transform.localPosition = Vector3.zero;
        controller.FieldsCardOrganize();

        yield return new WaitForSeconds(0);

        card1.transform.DOScale(Vector3.one * 2.2f, 0.3f);
        card2.transform.DOScale(Vector3.one * 2.2f, 0.3f);

        ghost1_sprite.DOFade(0, 0.5f);
        ghost2_sprite.DOFade(0, 0.5f);

        yield return new WaitForSeconds(0.3f);
        card1.transform.DOScale(Vector3.one * 2f, 0.2f);
        card2.transform.DOScale(Vector3.one * 2f, 0.2f);

    }
}
