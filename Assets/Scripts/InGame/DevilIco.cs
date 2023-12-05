using Data;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DevilIco : Card
{
    GameObject loveletter_effect = null;
    bool colorChange;

    void Awake()
    {
        if (loveletter_effect == null)
            loveletter_effect = Resources.Load<GameObject>("Prefebs/Effect/ghost");
        transform.name = "DevilIco";
    }

    private void Update()
    {
        ColorChange();
    }

    public void ColorChange()
    {
        GameController controller = GameController.GetInstance();
        if(controller.player_one_topCard == this || controller.player_ten_topCard == this)
        {
            colorChange = true;
            Card[] cards = controller.player_hand.GetComponentsInChildren<Card>();

            for(int i = 0; i < cards.Length - 1; i++)
            {
                cards[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else
        {
            if(colorChange)
            {
                Debug.Log("off");
                colorChange = false;

                Card[] cards = controller.player_hand.GetComponentsInChildren<Card>();

                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                }
            }
        }
        
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.One)
            StartCoroutine(DevilIcoSkill(controller.player_one_topCard, controller.opponent_one.transform));
        else
            StartCoroutine(DevilIcoSkill(controller.player_ten_topCard, controller.opponent_ten.transform));

        int targetId = GameController.GetInstance().playerID == 0 ? 1 : 0;
        SendServerMessage(GameController.GetInstance().playerID, (int)digit, targetId, 0, 0);
    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);

        GameController controller = GameController.GetInstance();

        if (digit == Digit.One)
            StartCoroutine(DevilIcoSkill(controller.opponent_one_topCard, controller.player_one.transform));
        else
            StartCoroutine(DevilIcoSkill(controller.opponent_ten_topCard, controller.player_ten.transform));
    }

    public IEnumerator DevilIcoSkill(Card card, Transform target)
    {
        SoundController.PlaySound("ani");

        yield return new WaitForSeconds(0.5f);
        SoundController.PlayEnvironment("Ingame/Change");
        card.transform.DOMove(target.position, 0.5f);

        card.GetComponent<SpriteRenderer>().sortingOrder = 1500;

        yield return new WaitForSeconds(0.5f);

        card.transform.parent = target;
        GameController.GetInstance().FieldsCardOrganize();

        bool isOpponent = target == GameController.GetInstance().player_one.transform || target == GameController.GetInstance().player_ten.transform;

        float rot = 45;
        if (isOpponent)
            rot = 225;

        for (int i = 0; i < 500; i++)
        {
            GameObject effect = Instantiate(loveletter_effect, GameController.GetInstance().effect_ts);
            Destroy(effect, 5);
            effect.transform.position = target.position;
            effect.transform.localScale = new Vector3(0, 0, 0);

            float delta = (Random.Range(0, 90) + rot) * Mathf.Deg2Rad;
            float distance = 60;

            effect.transform.up = new Vector3(Mathf.Cos(delta), Mathf.Sin(delta), 0);
            effect.transform.DOMove(effect.transform.position + new Vector3(Mathf.Cos(delta) * distance, Mathf.Sin(delta) * distance, 0), Random.Range(1.0f, 3.0f));
            effect.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }

        yield return new WaitForSeconds(3.0f);
    }
}
