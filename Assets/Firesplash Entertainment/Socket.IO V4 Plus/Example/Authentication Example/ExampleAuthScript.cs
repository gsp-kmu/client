using Firesplash.GameDevAssets.SocketIOPlus;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ExampleAuthScript : MonoBehaviour
{
    public SocketIOClient io;
    public Text uiStatus;

    [Serializable]
    struct AuthData
    {
        //This is just an example. You can design this object to amtch your needs.
        public string supersecret;
    }

    // Start is called before the first frame update
    void Start()
    {
        io.D.On("connect", () => {
            Debug.Log("LOCAL: Hey, we are connected!");
            uiStatus.text = "Socket.IO Connected.";

        });


        //The internal event connect_error is fired, when the server rejects our authentication (or something else propagates an error on the server side).
        //The payload of the connect_error event is always a JObject, it represents the JS "Error" Object created on the server side.
        //You could also cast to a custom structure here. We're using the JObject to showcase this quite universal variant.
        io.D.On<JObject>("connect_error", (jsErrorObject) => {
            Debug.Log("LOCAL: We received an error from the server: " + jsErrorObject.GetValue("message"));
            uiStatus.text = "Error: " + jsErrorObject.GetValue("message");
        });


        //When the conversation is done, the server will close our connection after 4 seconds
        io.D.On("disconnect", (reason) => {
            uiStatus.text = "Finished: " + reason;
        });


        //You can even transmit authentication payload to the server. This is done using a delegate.
        io.SetAuthPayloadCallback(GetAuthData);

        //We are now ready to actually connect
        //The simple way will use the parameters set in the inspector (or with a former call to Connect(...)):
        io.Connect();

    }


    /// <summary>
    /// This delegate is called by the library, when it requires authentication data for a namespace connect.
    /// We assigned this delegate a few lines higher.
    /// </summary>
    /// <param name="namespacePath">The namespace path ("/" for default)</param>
    /// <returns>should return the required payload object</returns>
    /// The server can access it using 
    object GetAuthData(string namespacePath)
    {
        if (namespacePath.Equals("/"))
        {
            Debug.Log("Delivering auth data for namespace /");
            return new AuthData()
            {
                supersecret = "UnityAuthenticationSample"
            };
        }
        return null;
    }
}
