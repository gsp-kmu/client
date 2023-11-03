using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public class NetworkData : MonoBehaviour
{
}


namespace Data{
    [Serializable]
    public struct Card
    {
        public string id;
        public string name;
        public string url;
    }

    // ���� ���� ������ Ȯ��
    public struct InGameTurn
    {
        public bool isPlayerTurn; // True�� ���� False�� ���� ��
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
        public string id;
        public Card card;
        public Digit drawDigit; // ���� int ������ �ٲ� ���� ����
        public string targetId; // �⺻������ ���� 0, 1�̸� ����
        public Digit targetDigit;
    }
    
    // ���� �̰�°� ǥ��
    public struct InGameEnd
    {
        public string winId;
    }
}