using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject canvasManager;
    public TMP_InputField idField;
    public TMP_InputField passwordField;
    public NetworkService networkService;

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
        string id = idField.text;
        User user = new User
        {
            id = id,
            password = passwordField.text
        };

        string json = JsonUtility.ToJson(user);

        StartCoroutine(LoginPost(json, id));
    }

    IEnumerator LoginPost(string json, string id)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.responseCode);
            if (request.responseCode == 200)
            {
                Debug.Log("로그인 성공");
                NetworkService.Instance.Login(id);
            }
            else
            {
                Debug.Log("로그인 실패");
            }
        }
    }
    public void ReturnToRegister()
    {
        canvasManager.GetComponent<CanvasManager>().SetCurrentPage(CanvasManager.mPageInfo.Register);
    }
}
