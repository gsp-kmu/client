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
        public int id;
    }

    public struct InGameTurn
    {
        public string isPlayerTurn; 
    }

    [Serializable]
    public struct FirstCard
    {
        public Card card1;
        public Card card2;
    }

    [Serializable]
    public struct PlayCard
    {
        public int id;
        public int cardIndex;
        public int drawDigit; // ���� int ������ �ٲ� ���� ����
        public int targetId; // �⺻������ ���� 0, 1�̸� ����
        public int targetDigit;
        public int targetCardIndex;
    }
    
    // ���� �̰�°� ǥ��
    public struct InGameEnd
    {
        public string winId;
    }
}