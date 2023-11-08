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

        SendServerMessage(GameController.GetInstance().playerID, 18, (int)digit, 0, 0, 0);
    }
    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

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
