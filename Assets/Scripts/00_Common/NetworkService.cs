using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;
using System;
using UnityEngine.Events;

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

    public void Login(string id)
    {
        io.Connect();
        io.D.On("connect", () =>
        {
            Debug.Log("connect");
            io.D.Emit("들어왔습니다");
            Send("login", id);
        });

        eventHandler = new NetworkEventHandler(io, eventCallback);
    }

    private void OnApplicationQuit()
    {
        io.D.Emit("disconnect");
    }

    public void RemoveEvent(string eventName)
    {
        io.D.RemoveAllListeners(eventName);
    }

    public void AddEvent(string eventName, UnityAction callback)
    {
        io.D.On(eventName, callback);
    }

    public void AddEvent<T>(string eventName, UnityAction<T> callback)
    {
        io.D.On<T>(eventName, callback);
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
