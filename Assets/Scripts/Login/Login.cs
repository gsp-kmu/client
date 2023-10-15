using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class User
{
    public string id;
    public string password;
}
public class Login : MonoBehaviour
{
    public string loginUrl;
    public string registerUrl;
    public TextMeshPro idField;
    public TextMeshPro passwordField;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginButtonClick()
    {
        User user = new User
        {
            id = idField.text,
            password = passwordField.text
        };

        string json = JsonUtility.ToJson(user);

        StartCoroutine(LoginPost(json));
    }

    IEnumerator LoginPost(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log("로그인 성공");
            }
            else
            {
                Debug.Log(request.error.ToString());
            }
        }
    }
    public void RegisterButtonClick()
    {
        User user = new User
        {
            id = "mclub4",
            password = "12345678"
        };

        string json = JsonUtility.ToJson(user);

        StartCoroutine(RegisterPost(json));
    }

    IEnumerator RegisterPost(string json)
    {
        using(UnityWebRequest request = UnityWebRequest.PostWwwForm(registerUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.error != null)
            {
                Debug.Log("회원가입 성공");
            }
            else
            {
                Debug.Log(request.error.ToString());
            }
        }
    }
}
