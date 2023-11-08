using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Elf : Card
{
    static GameObject[] elf_effect = null;
    void Awake()
    {
        if (elf_effect == null)
        {
            elf_effect = new GameObject[3];
            for (int i = 0; i < elf_effect.Length; i++)
                elf_effect[i] = Resources.Load<GameObject>("Prefebs/Effect/Elf_effect_" + i.ToString());
        }
        transform.name = "Elf";
    }

    public override void BattleCry(Digit digit)
    {
        base.BattleCry(digit);

        GameController controller = GameController.GetInstance();

        SendServerMessage(controller.playerID, 19, (int)digit, 0, 0, 0);

        if (digit == Digit.Ten)
            StartCoroutine(Spawn(controller.player_ten.transform));
        else
            StartCoroutine(Spawn(controller.player_one.transform));

    }

    public override void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        base.BattleCryOpponent(digit, target, target_digit, targetCardIndex);
        GameController controller = GameController.GetInstance();

        if (digit == Digit.Ten)
            StartCoroutine(Spawn(controller.opponent_ten.transform));
        else
            StartCoroutine(Spawn(controller.opponent_one.transform));
    }

    static IEnumerator Spawn(Transform pos)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject effect = Instantiate(elf_effect[Random.Range(0, 3)], GameController.GetInstance().effect_ts);
            float pos_r = Random.Range(5.0f, 10);
            float ros_r = Random.Range(0, Mathf.PI * 2);

            effect.transform.position = new Vector3(Mathf.Cos(ros_r), Mathf.Sin(ros_r)) * pos_r + pos.position;
            effect.transform.localScale = Vector3.zero;
            effect.transform.DOScale(Vector3.one * 2.0f, 1.0f);
            effect.transform.DOPunchRotation(Vector3.forward * 180, 1.0f);
            effect.GetComponent<SpriteRenderer>().DOFade(0, 1.3f);
            Destroy(effect, 1.5f);
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
