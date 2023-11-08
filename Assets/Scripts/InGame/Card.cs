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

    public virtual void BattleCryOpponent(Digit digit, int target, Digit target_digit, int targetCardIndex)
    {
        Debug.Log("상대방 " + transform.name + " 능력발동");
    }

    public void SendServerMessage(int id, int cardIndex, int drawDigit, int targetId, int targetDigit, int targetIndex)
    {
        Data.PlayCard send_card = new Data.PlayCard();
        send_card.id = id;
        send_card.cardIndex = cardIndex;
        send_card.drawDigit = drawDigit;
        send_card.targetId = targetId;
        send_card.targetDigit = targetDigit;
        send_card.targetCardIndex = targetIndex;
        NetworkService.Instance.Send(NetworkEvent.INGAME_PLAY_CARD, send_card);
        NetworkService.Instance.Send(NetworkEvent.INGAME_TURN_END, "");
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
