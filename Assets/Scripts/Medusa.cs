using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Medusa : Card
{
    static GameObject effect;

    void Start()
    {
        effect = Resources.Load<GameObject>("Prefebs/Effect/Medusa_effect");
    }

    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        StartCoroutine(Stun(transform));

        Data.PlayCard send_card = new Data.PlayCard();
        send_card.id = "";
        send_card.card.id = "7";
        send_card.drawDigit = digit; // 추후 int 형으로 바뀔 수도 있음
        send_card.targetId = "0"; // 기본적으로 값은 0, 1이면 상대방
        send_card.targetDigit = digit;
        //NetworkService.Instance.Send(NetworkEvent.INGAME_DRAW_CARD, send_card);
    }
    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        base.BattleCryOpponent(digit, target, target_digit);

        StartCoroutine(Stun(transform));
    }

    static IEnumerator Stun(Transform tf)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject effect_obj = Instantiate(effect, tf);
        Image effect_img = effect_obj.GetComponentInChildren<Image>();
        effect_img.DOFade(0.8f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        effect_img.DOFade(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
    }
}
