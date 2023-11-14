using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

[System.Serializable]
public class ResponseGetRandomCard
{
    public string msg;
    public List<int> cardList;
}



public class RandomSelect : MonoBehaviour
{
    private string getCardRandomUrl = "http://ec2-43-201-164-245.ap-northeast-2.compute.amazonaws.com:8000/random";
    public List<Card_> deck = new List<Card_>();  // 카드 덱
    private List<int> deckIndex = new List<int>();
    public int total = 0;  // 카드들의 가중치 총 합
    public GameObject gachabutton;
    public List<Vector3> cardpo = new List<Vector3>(); //카드 위치

    public void Start()
    {
        gachabutton.GetComponent<Button>().enabled = false;
        int id = 1;
        RequestGetDeck deck = new RequestGetDeck
        {
            userId = id
        };

        string json = JsonUtility.ToJson(deck);

        StartCoroutine(CardRandomGet(json));
        
    }
    public List<Card_> result = new List<Card_>();  // 랜덤카드 리스트

    public Transform parent;
    public GameObject cardprefab;
    public List<GameObject> cardUIs = new List<GameObject>();


    public void ResultSelect()
    {
        for (int i = 0; i < 5; i++)
        {
            result.Add(RandomCard(i)); 
            // 비어있는 카드를 생성
            GameObject card = Instantiate(cardprefab, parent);
            CardUI cardUI = card.GetComponent<CardUI>();
            card.transform.localPosition = cardpo[i];
            // 카드에 각종정보 추가
            cardUI.CardUISet(result[i]);
            cardUIs.Add(card);
        }
        total = 0;
    }
    public Card_ RandomCard(int i)
    {
 
        Card_ temp = new Card_(deck[deckIndex[i]]);
        return temp;
    }

    IEnumerator CardRandomGet(string json)
    {

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(getCardRandomUrl, json))
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
                Debug.Log("랜덤 카드 받아오기 성공");

                Debug.Log(responseJson);
                ResponseGetRandomCard response = JsonConvert.DeserializeObject<ResponseGetRandomCard>(responseJson);


                Debug.Log(response.msg);
                foreach (var sub in response.cardList)
                {
                    Debug.Log(sub);
                }
                deckIndex = response.cardList;
                ResultSelect();


            }
            else if (request.responseCode == 400)
            {
                Debug.Log("로그인 실패");
            }
        }
    }


}
