using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public class NetworkData : MonoBehaviour
{
}

namespace Data
{
    [Serializable]
    public struct Card
    {
        public string id;
        public string name;
        public string url;
    }

    // 누가 먼저 턴인지 확인
    public struct InGameTurn
    {
        bool isPlayerTurn; // True면 내턴 False면 상대방 턴
    }

    // 첫 카드 2장 뽑기
    public struct FIrstCard
    {
        Card card1;
        Card card2;
    }

    // 카드 드로우
    public struct DrawCard
    {
        public string id;
        public Card card;
        public Digit drawDigit; // 추후 int 형으로 바뀔 수도 있음
        public string targetId; // 기본적으로 값은 0, 1이면 상대방
        public Digit targetDigit;
    }
    struct PlayCard
    {
        public string id;
        public Card card;
        public Digit drawDigit; // 추후 int 형으로 바뀔 수도 있음
        public string targetId; // 기본적으로 값은 0, 1이면 상대방
        public Digit targetDigit;
    }

    // 누가 이겼는가 표시
    public struct InGameEnd
    {
        string winId;
    }
}