using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCheatService : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;

    public void SendCheatDraw()
    {
        Data.Card card;
        card.id = int.Parse(inputField.text);
        NetworkService.Instance.Send("cheat_ingame_draw_card", card);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SendCheatDraw();
        }
    }
}
