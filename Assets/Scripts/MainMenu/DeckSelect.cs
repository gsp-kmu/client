using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class ResponseGetCoin
{
    public string msg;
    public int coin;
}
public class DeckSelect : MonoBehaviour
{
    public int currentIdx = 0;
    public Image[] decks;
    public Vector3 moveleft;
    public Vector3 movecenter;
    public Vector3 moveright;
    public float moveTime;
    public RectTransform[] rectTransforms;
    private List<string> decknameList;
    public TextMeshProUGUI coinGUI;
    public bool[] isDeckAtive = { false, false, false, false, false };

    public CardShaderSelector cardShaderSelector;
    private string getDeckUrl = GSP.http.getDeck;
    private string getCoinUrl = GSP.http.getCoin;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(NetworkService.Instance.id);
        RequestGetDeck deck = new RequestGetDeck
        {
            userId = int.Parse(NetworkService.Instance.id)
        };
        string json = JsonUtility.ToJson(deck);
        StartCoroutine(DeckGet(json));
        StartCoroutine(CoinGet(json));
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
                Debug.Log("�� �޾ƿ��� ����");

                Debug.Log(responseJson);
                ResponseGetDeck response = JsonConvert.DeserializeObject<ResponseGetDeck>(responseJson);

                Debug.Log(response.msg);
                decknameList = response.nameList;
                Debug.Log(response);
                for (int i = 0; i < 5; i++)
                {
                    TextMeshProUGUI text = decks[i].GetComponentInChildren<TextMeshProUGUI>();
                    if(response.deckList[i].Count != 20){
                        text.color = new Color(0.66f, 0.66f, 0.66f);
                        cardShaderSelector.SetAtiveBlackWhiteCardShader(decks[i]);
                    }
                    else
                    {
                        isDeckAtive[i] = true;
                    }
                    if (text != null)
                    {
                        text.text = decknameList[i];
                    }
                }
            }
            else if (request.responseCode == 400)
            {
                Debug.Log("�� �޾ƿ��� ����");
            }
        }
        yield return null;
    }
    IEnumerator CoinGet(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(getCoinUrl, json))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.responseCode);

            int coin = 0;
            if (request.responseCode == 200)
            {
                string responseJson = request.downloadHandler.text;
                Debug.Log("���� �޾ƿ��� ����");

                Debug.Log(responseJson);
                ResponseGetCoin response = JsonConvert.DeserializeObject<ResponseGetCoin>(responseJson);

                Debug.Log(response.msg);
                
                coin = response.coin;
            }
            else if (request.responseCode == 400)
            {
                Debug.Log("�� �޾ƿ��� ����");
            }

            coinGUI.text = coin.ToString();
        }
        yield return null;
    }
    public void ClickLeftCard()
    {
        StartCoroutine(moveRight());
    }

    public void ClickRightCard()
    {
        StartCoroutine(moveLeft());
    }

    IEnumerator moveLeft()
    {
        if (currentIdx != 4)
        {
            Debug.Log(currentIdx + " OK");
            rectTransforms[currentIdx].DOAnchorPos3D(moveleft, moveTime);
            rectTransforms[currentIdx + 1].DOAnchorPos3D(movecenter, moveTime);
            currentIdx++;
        }

        yield return null;
    }

    IEnumerator moveRight()
    {
        if (currentIdx != 0)
        {
            rectTransforms[currentIdx].DOAnchorPos3D(moveright, moveTime);
            rectTransforms[currentIdx - 1].DOAnchorPos3D(movecenter, moveTime);
            currentIdx--;
        }

        yield return null;
    }
}
