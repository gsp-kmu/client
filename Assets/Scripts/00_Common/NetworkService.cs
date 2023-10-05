using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;

public class NetworkService : MonoBehaviour
{
    public SocketIOClient io;
    public NetworkEventCallback eventCallback;
    // Start is called before the first frame update
    void Start()
    {
        io.Connect();
        io.D.On("connect", () =>
        {
            Debug.Log("connect");
            io.D.Emit("들어왔습니다");
        });
        io.D.On(NetworkEvent.TEST_MESSAGE, (string message)=>
        {
            eventCallback.PrintMessage(message);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
