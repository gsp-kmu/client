using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DeckSelect : MonoBehaviour
{
    public int currentId = 1;
    public Image[] decks;
    public List<string> decknameList;
    private string getDeckUrl = GSP.http.getDeck;

    // Start is called before the first frame update
    void Start()
    {
        int id = 1;
        RequestGetDeck deck = new RequestGetDeck
        {
            userId = id
        };
        string json = JsonUtility.ToJson(deck);
        yield return StartCoroutine(DeckGet(json));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DeckGet(string json)
    {

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(getDeckUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.responseCode);
            if (request.responseCode == 200)
            {
                string responseJson = request.downloadHandler.text;
                Debug.Log("? ???? ??");

                Debug.Log(responseJson);
                ResponseGetDeck response = JsonConvert.DeserializeObject<ResponseGetDeck>(responseJson);


                Debug.Log(response.msg);
                decknameList = response.nameList;
            }
            else if (request.responseCode == 400)
            {
                Debug.Log("? ???? ??");
            }
        }
        yield return null;
    }
}