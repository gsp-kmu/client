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
    public string id;
    public NetworkEventCallback eventCallback;
    private NetworkEventHandler eventHandler;


    /// 임시
    public Notice noticeObject;
    /// 임시
    private void Awake()
    {
        io.serverAddress = GSP.Info.ip;

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Login(string id, UnityAction successCallback, UnityAction failCallback)
    {
        Debug.Log(io.serverAddress);
        io.Connect();
        io.D.On("connect", () =>
        {
            Debug.Log("connect");
            io.D.Emit("들어왔습니다");
            Send("login", id);
            
            AddEvent("login_success", (string data) =>
            {
                successCallback();
            });

            AddEvent("login_fail", (string data) =>
            {
                failCallback();
            });

            AddEvent("initid", (string id) =>
            {
                Debug.Log(id);
                this.id = id;
            });

            AddEvent("initid", (string id) =>
            {
                Debug.Log(id);
                this.id = id;
            });

            AddEvent("notice", (string data) =>
            {
                Instantiate(noticeObject).GetComponent<Notice>().Init(data);
            });
        });

        eventHandler = new NetworkEventHandler(io, eventCallback);
    }

    [Obsolete("옛날 버전 이니까 되도록 fail callback 추가된 새로운 로그인 쓰세요.(이것도 작동되긴함)")]
    public void Login(string id, UnityAction successCallback)
    {
        Login(id, successCallback, () => { });
    }
    public void Login(string id)
    {
        Login(id, () => { });
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