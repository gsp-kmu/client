using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
using static System.Net.WebRequestMethods;
using UnityEngine.Networking;
using System;
using UnityEditor.PackageManager.Requests;
using Newtonsoft.Json;

public class RequestGetDeck
{
    public int userId;
}

[System.Serializable]
public class ResponseGetCard
{
    public string msg;
    public List<int> cardList;
}

[System.Serializable]
public class ResponseGetDeck
{
    public string msg;
    public List<List<int>> deckList;
}
public class CardDeck : MonoBehaviour
{
    private string getDeckUrl = "http://ec2-43-201-164-245.ap-northeast-2.compute.amazonaws.com:8000/getdeck";
    private string getCardUrl = "http://ec2-43-201-164-245.ap-northeast-2.compute.amazonaws.com:8000/getcard";

    public List<List<int>> allDecks; // ���� ���� �����ϴ� ����Ʈ�� ����Ʈ
    public List<List<int>> allCardState; // ���ī�� ����

    public List<int> currentDeck; // ���� Ȱ��ȭ�� ��
    public List<int> currentCollectionCards; // ���� ��� ī��
    public List<int> currentCardState; //���� ī�� ����


    public List<GameObject> allCardDeckCollect; // ī��� �ѹ������ֱ� ���� ���� ī�� ������Ʈ ����Ʈ
    public List<Button> deckButtons; // �� ��ȯ�� ���� ��ư ����Ʈ
    private int previousindex = 0;
    public Sprite[] sprites;
    public List<int> reciveCardIndex = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
    int num = 0; // ī�� ������Ͽ�

    void Start()
    {
        int id = 1;
        RequestGetDeck deck = new RequestGetDeck
        {
            userId = id
        };
        string json = JsonUtility.ToJson(deck);
        StartCoroutine(DeckAndCardGet(json));
    }

    IEnumerator DeckAndCardGet(string json)
    {
        yield return StartCoroutine(DeckGet(json));
        yield return StartCoroutine(CardGet(json));

        // �� �ڷ�ƾ�� �Ϸ�� �Ŀ� ������ �Լ�
        UpdateAllCardsUI();
        Debug.Log("UpdateAllCardsUI ����");
    }

    // �� ��ȯ �̺�Ʈ �ڵ鷯
    private void SwitchDeck(int deckIndex)
    {
        if (deckIndex >= 0 && deckIndex < deckButtons.Count)
        {

            deckButtons[previousindex].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
            // ���� ����Ʈ�� �����Ͽ� ���ο� ����Ʈ�� ����
            List<int> newCardState = new List<int>(currentCardState);
            List<int> newCardDeck = new List<int>(currentDeck);

            // ���� ����Ʈ�� ���ο� ����Ʈ�� �Ҵ�
            allCardState[previousindex] = newCardState;
            allDecks[previousindex] = newCardDeck;

            deckButtons[deckIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            currentCardState = allCardState[deckIndex];
            currentDeck = allDecks[deckIndex];
            switchCard();
            previousindex = deckIndex;
        }
        else
        {
            Debug.LogError("��ȿ���� ���� �� �ε����Դϴ�.");
        }


    }



    private void UpdateAllCardsUI()
    {
        sprites = Resources.LoadAll<Sprite>("AllCard");
        if (sprites.Length > 0)
        {
            // ���ϴ� ��������Ʈ�� ����� �� ����
        }
        else
        {
            Debug.LogError("��������Ʈ�� ã�� �� �����ϴ�.");
        }
        for (int i = 0; i < currentCollectionCards.Count; i++)
        {
            currentCollectionCards[i] = currentCollectionCards[i] - 1;
        }
        allCardState = new List<List<int>>();

        for (int i = 0; i < 5; i++)
        {
            List<int> subList = new List<int>(new int[currentCollectionCards.Count]);
            subList = findState(allDecks[i], subList);
            allCardState.Add(subList);
        }
        currentCardState = allCardState[0];
        foreach (int i in currentCollectionCards)
        {

            allCardDeckCollect.Add(generateCard(i, num));
            num++;
        }

        for (int i = 0; i < allCardDeckCollect.Count; i++)
        {
            GameObject deckcard = allCardDeckCollect[i];
            if (currentCardState[i] == 0)
            {
                setParentAllCard(deckcard);
            }
            else
            {
                setParentMyDeckCard(deckcard, allCardDeckCollect[i].name);
            }
        }
        for (int i = 0; i < deckButtons.Count; i++)
        {
            int index = i;
            deckButtons[i].onClick.AddListener(() => SwitchDeck(index));
        }
    }

    private GameObject generateCard(int i, int num)
    {
        GameObject deckcard = new GameObject(i.ToString());
        RectTransform rectTransform = deckcard.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(80f, 100f);
        rectTransform.localScale = new Vector3(3f, 3f, 3f);
        rectTransform.pivot = new Vector2(0f, 1f);

        // CanvasRenderer ������Ʈ �߰� (UI ������Ʈ�� �ʿ��� ������Ʈ)
        CanvasRenderer canvasRenderer = deckcard.AddComponent<CanvasRenderer>();

        CardClick cardclick = deckcard.AddComponent<CardClick>();
        cardclick.num = num;

        // Image ������Ʈ �߰�
        Image imageComponent = deckcard.AddComponent<Image>();
        imageComponent.sprite = sprites[i];
        deckcard.tag = i.ToString();

        return deckcard;
    }

    private void setParentAllCard(GameObject deckcard)
    {
        deckcard.transform.localScale = new Vector3(3f, 3f, 3f);
        if (deckcard.transform.parent != GameObject.FindGameObjectWithTag("AllCard").transform)
        {
            deckcard.transform.SetParent(GameObject.FindGameObjectWithTag("AllCard").transform);
            deckcard.transform.position = GameObject.FindGameObjectWithTag("AllCard").transform.position;
        }
    }

    private void setParentMyDeckCard(GameObject deckcard, string i)
    {
        if(i.Length == 2)
        {
            i = i[1].ToString();
        }
        GameObject cardin = GameObject.Find("card0/" + i);
        GameObject cardin2 = GameObject.Find("card1/" + i);
        if (deckcard.transform.parent != cardin.transform && deckcard.transform.parent != cardin2.transform)
        {
            deckcard.transform.localScale = new Vector3(1f, 1f, 1f);
            if (cardin.transform.childCount == 0)
            {
                deckcard.transform.localScale = new Vector3(1f, 1f, 1f);
                deckcard.transform.SetParent(cardin.transform);
                deckcard.transform.position = cardin.transform.position;

            }
            else if (cardin2.transform.childCount == 0)
            {
                deckcard.transform.localScale = new Vector3(1f, 1f, 1f);
                deckcard.transform.SetParent(cardin2.transform);
                deckcard.transform.position = cardin2.transform.position;
            }
            else
            {
                deckcard.transform.localScale = new Vector3(1f, 1f, 1f);
                Debug.Log(i + "����" + cardin.transform.childCount);
                Debug.Log(cardin.transform.name);
            }
        }

    }

    private void switchCard()
    {
        for (int i = 0; i < allCardDeckCollect.Count; i++)
        {
            GameObject deckCard = allCardDeckCollect[i];
            if (currentCardState[i] == 0)
            {
                setParentAllCard(deckCard);
                deckCard.transform.localScale = new Vector3(3f, 3f, 3f);
            }
            else
            {
                setParentMyDeckCard(deckCard, allCardDeckCollect[i].name);
                deckCard.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    private List<int> findState(List<int> deck, List<int> sublist)
    {
        foreach (int i in deck)
        {
            int indeck = currentCollectionCards.IndexOf(i - 1);
            if (sublist[indeck] == 0)
            {
                sublist[indeck] = 1;
                Debug.Log("ã�¼���"+i+  " �ε���:"+ indeck);
            }
            else
            {
                sublist[currentCollectionCards.IndexOf(i - 1, indeck + 1)] = 1;
                Debug.Log("ã�¼���" + i + " �ε���:" + currentCollectionCards.IndexOf(i - 1, indeck + 1));
            }
        }
        return sublist;
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
                allDecks = response.deckList;
                currentDeck = allDecks[0];

            }
            else if (request.responseCode == 400)
            {
                Debug.Log("�� �޾ƿ��� ����");
            }
        }
        yield return null;
    }

    IEnumerator CardGet(string json)
    {

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(getCardUrl, json))
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
                Debug.Log("ī�� �޾ƿ��� ����");

                Debug.Log(responseJson);
                ResponseGetCard response = JsonConvert.DeserializeObject<ResponseGetCard>(responseJson);


                Debug.Log(response.msg);
                currentCollectionCards = response.cardList;


            }
            else if (request.responseCode == 400)
            {
                Debug.Log("ī�� �޾ƿ��� ����");
            }
        }
        yield return null;
    }




}


