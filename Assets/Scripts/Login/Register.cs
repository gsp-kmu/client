using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    private string registerUrl = GSP.http.register;
    public TMP_InputField idField;
    public TMP_InputField passwordField;
    public GameObject canvasManager;

    public Toggle isAgree;
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
        ButtonClick.instance.PlayButtonClick();
        if (isAgree.isOn == false)
        {
            canvasManager.GetComponent<CanvasManager>().SetPopup(CanvasManager.mPageInfo.Register, 
                CanvasManager.errorInfo.agreeError);
            return;
        }

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
                Debug.Log("굳 ");
                canvasManager.GetComponent<CanvasManager>().SetPopup(CanvasManager.mPageInfo.Register, CanvasManager.errorInfo.success);
            }
            else
            {
                CanvasManager.errorInfo error = CanvasManager.errorInfo.NetworkError;
                if (request.responseCode == 400)
                {
                    error = CanvasManager.errorInfo.patternError;
                }
                else if (request.responseCode == 401)
                {
                    error = CanvasManager.errorInfo.duplicateError;
                }

                canvasManager.GetComponent<CanvasManager>().SetPopup(CanvasManager.mPageInfo.Register, error);
            }
        }
    }
}
