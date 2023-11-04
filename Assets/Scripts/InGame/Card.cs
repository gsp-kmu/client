using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int num;
    public Digit digit;

    public virtual void BattleCry(Digit digit)
    {
        Debug.Log(transform.name + " 능력발동");
    }

    public virtual void BattleCryOpponent(Digit digit, int target, Digit target_digit)
    {
        Debug.Log("상대방 " + transform.name + " 능력발동");
    }

    public IEnumerator PlayCard(Transform digit_ts)
    {
        transform.parent = digit_ts.transform;
        GameController.GetInstance().FieldCardOrganize(transform.parent);

        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DOScale(Vector3.one * 2.2f, 0.3f);

        yield return new WaitForSeconds(0.3f);
        transform.DOScale(Vector3.one * 2f, 0.2f);
    }
}
