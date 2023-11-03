using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkEvent
{
    public const string TEST_MESSAGE = "test-message";
    public const string TEST_CARD = "test-card";

    //게임 시작
    public const string INGAME_TURN = "ingame_turn";
    public const string INGAME_FIRST_CARD = "ingame_first_card";
    public const string INGAME_DRAW_CARD = "ingame_draw_card";
    public const string INGAME_END = "ingame_end";
    public const string INGAME_WIN_LOSE = "ingame_win_lose";
    public const string INGAME_PLAY_CARD = "ingame_play_card";

    //상점
    public const string SHOP_RANDOM_DRAWCARD = "shop_random_drawcard";

    //매칭
    public const string MATCH_START = "match_start";
    public const string MATCH_SUCCESS = "match_success";
    public const string MATCH_CANCEL = "match_cancel";
    public const string MATCH_END = "match_end";
}