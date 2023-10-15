using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public class NetworkData : MonoBehaviour
{
}

enum Digit
{
    one,
    ten,
}

namespace Data{
    [Serializable]
    public struct Card{
        public string id;
        public string name;
        public string url;
    }

    // ���� ���� ������ Ȯ��
    public struct InGameTurn
    {
        bool isPlayerTurn; // True�� ���� False�� ���� ��
    }
    
    // ù ī�� 2�� �̱�
    public struct FIrstCard
    {
        Card card1;
        Card card2;
    }

    // ī�� ��ο�
    public struct DrawCard
    {
        string id;
        Card card;
        Digit drawDigit; // ���� int ������ �ٲ� ���� ����
        string targetId; // �⺻������ ���� 0, 1�̸� ����
        Digit targetDigit;
    }
    
    // ���� �̰�°� ǥ��
    public struct InGameEnd
    {
        string winId;
    }
}
