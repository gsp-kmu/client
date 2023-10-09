using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;

public class NetworkService : MonoBehaviour
{
    private static NetworkService instance = null;
    public static NetworkService Instance { get { return instance; } }

    public SocketIOClient io;
    public NetworkEventCallback eventCallback;
    private NetworkEventHandler eventHandler;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        io.Connect();
        io.D.On("connect", () =>
        {
            Debug.Log("connect");
            io.D.Emit("들어왔습니다");
        });

        eventHandler = new NetworkEventHandler(io, eventCallback);
    }

    public void Send(string eventName, string message)
    {
        Send<string>(eventName, message);
    }

    public void Send<T>(string eventName, T message)
    {
        io.D.Emit<T>(eventName, message);
    }
}
