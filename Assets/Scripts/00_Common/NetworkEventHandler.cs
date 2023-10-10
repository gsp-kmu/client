using Firesplash.GameDevAssets.SocketIOPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEventHandler
{
    private SocketIOClient io;
    private NetworkEventCallback eventCallback;

    public NetworkEventHandler(SocketIOClient io, NetworkEventCallback eventCallback)
    {
        this.io = io;
        this.eventCallback = eventCallback;

        Init();
    }

    private void Init()
    {
        io.D.On(NetworkEvent.TEST_MESSAGE, (string message) =>
        {
            eventCallback.PrintMessage(message);
        });
    }
}
