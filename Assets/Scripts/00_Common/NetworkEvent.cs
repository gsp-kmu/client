using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkEvent
{
    public const string TEST_MESSAGE = "test-message";
    public const string TEST_CARD = "test-card";

    //���� ����
    public const string INGAME_CLIENT_READY = "ingame_client_ready";
    public const string INGAME_TURN = "ingame_turn";
    public const string INGAME_INIT_ID = "ingame_init_id";
    public const string INGAME_TURN_END = "ingame_turn_end";
    public const string INGAME_FIRST_CARD = "ingame_first_card";
    public const string INGAME_DRAW_CARD = "ingame_draw_card";
    public const string INGAME_PLAY_RECV = "ingame_play_recv"; // hyeonseo
    public const string INGAME_PLAY_SEND = "ingame_play_send"; // hyeonseo
    public const string INGAME_END_WIN = "ingame_end_win";
    public const string INGAME_END_LOSE = "ingame_end_lose";
    public const string INGAME_SURRENDER = "ingame_surrender";
    //����
    public const string SHOP_RANDOM_DRAWCARD = "shop_random_drawcard";

    //��Ī
    public const string MATCH_START = "match_start";
    public const string MATCH_SUCCESS = "match_success";
    public const string MATCH_CANCEL = "match_cancel";
    public const string MATCH_END = "match_end";

}