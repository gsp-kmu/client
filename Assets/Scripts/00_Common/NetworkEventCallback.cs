using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;
using System;

public class NetworkEventCallback : MonoBehaviour
{
    public void PrintMessage(string message)
    {
        Debug.Log("테스트 메시지: " + message.ToString());
    }
}
