using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public string registerUrl;
    public TMP_InputField idField;
    public TMP_InputField passwordField;
    public GameObject canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToLogin()
    {
        canvasManager.GetComponent<CanvasManager>().SetCurrentPage(CanvasManager.mPageInfo.Login);
    }

    public void RegisterButtonClick()
    {
        User user = new User
        {
            id = idField.text,
            password = passwordField.text
        };

        string json = JsonUtility.ToJson(user);

        StartCoroutine(RegisterPost(json));
    }

    IEnumerator RegisterPost(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(registerUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.responseCode);
            if (request.responseCode == 200)
            {
                Debug.Log("회원가입 성공");
            }
            else
            {
                Debug.Log("회원가입 실패");
            }
        }
    }
}
