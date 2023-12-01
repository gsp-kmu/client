using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;
using DG.Tweening;
using TMPro;
using Sequence = DG.Tweening.Sequence;
using System.Linq;

[System.Serializable]
public class ResponseGetRandomCard
{
    public string msg;
    public List<int> cardList;
    public List<bool> duplicate;
    public int coin;
}
public class RequestSendCoin
{
    public int userId;
}


public class RandomSelect : MonoBehaviour
{
    private string getCardRandomUrl = GSP.http.random;
    

    public int mycoin;
    public GameObject coinText;

    private bool isSkipping = false;

    public List<Card_> deck = new List<Card_>();  // 카드 덱
    private List<int> deckIndex = new List<int>();
    public int total = 0;  // 카드들의 가중치 총 합
    public GameObject gachaButton;
    public GameObject succesButton;
    public GameObject failButton;
    List<int> randomRecieve = new List<int>(){ 11, 2, 3, 4, 5 };
    public List<bool> cardSamebool = new List<bool>() { true, false, true, false, false };

    public List<Vector3> cardpo = new List<Vector3>(); //카드 위치
    public float radius = 10f;
    public float duration = 5f;
    public AudioClip[] cardGachaSound;
    List<List<Vector3>> pathS = new List<List<Vector3>>
{
    new List<Vector3> { new Vector3(-150, -300, 0), new Vector3(-300, 100, 0) },
    new List<Vector3> { new Vector3(300, -150, 0), new Vector3(-100, -300, 0) },
    new List<Vector3> { new Vector3(250, 420, 0), new Vector3(300, -300, 0) },
    new List<Vector3> { new Vector3(-250, 420, 0), new Vector3(300, 300, 0) },
    new List<Vector3> { new Vector3(-300, 150, 0), new Vector3(100, 300, 0) }
    // 원하는 만큼 Vector3 리스트를 추가할 수 있습니다.
};

    public void SkipAnimations()
    {
        if (isSkipping == false)
        {
            int i = 0;
            // 모든 카드 애니메이션 즉시 완료
            foreach (var card in cardUIs)
            {
                card.transform.DOKill(); // 해당 카드의 모든 Tweens를 즉시 중지
                                         // 트윈 애니메이션의 Complete를 호출하여 애니메이션을 즉시 완료
                card.transform.DOLocalPath(pathS[i].ToArray(), 0.001f).Complete();
                card.GetComponent<CardUI>().SkipAnimations();
                i++;
            }
        }
        isSkipping = true;
    }

    public void OnEnable()
    {
        gachaButton.GetComponent<Button>().enabled = false;

        //// id 입력받는곳/////
        int id = int.Parse(NetworkService.Instance.id);
        RequestGetDeck deck = new RequestGetDeck
        {
            userId = id
        };
        

        string json1 = JsonUtility.ToJson(deck);
        
        //local test
        //deckIndex = randomRecieve;
        //ResultSelect();
        //failButton.SetActive(false);

        StartCoroutine(CardRandomGet(json1));

    }


    public void OnDisable()
    {
        gachaButton.GetComponent<Button>().enabled = true;
        isSkipping = false;
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

            AudioSource cardSound = card.GetComponent<AudioSource>().AddComponent<AudioSource>();
            
            cardSound.clip = cardGachaSound[0];
            cardSound.PlayDelayed(0.5f * i);

            Vector3 targetPosition = cardpo[i];
            List<Vector3> path = pathS[i];
            path.Add(targetPosition);
            Vector3[] paths = path.ToArray();
            pathS[i] = path;

            // 카드가 나선 모양의 경로를 따라 움직이도록 설정
            
            Tween cardMovement = card.transform.DOLocalPath(paths, 1f, PathType.CatmullRom)
            .SetEase(Ease.OutCubic)
            .SetDelay(i*0.5f)
            .OnComplete(() =>
            {
                // 카드가 움직인 후에 흔들림 효과 주기
                

            });

            card.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (cardSamebool[i] == true)
            {
                card.transform.Find("중복").gameObject.GetComponent<TextMeshProUGUI>().text="same";
            }
            // 카드에 각종정보 추가
            cardUI.CardUISet(result[i]);
            cardUIs.Add(card);
        }
        total = 0;
    }

 
    public Card_ RandomCard(int i)
    {
 
        Card_ temp = new Card_(deck[deckIndex[i]-1]);
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
                deckIndex = response.cardList;
                cardSamebool = response.duplicate;
                mycoin = response.coin;
                coinText.GetComponent<TextMeshProUGUI>().text = mycoin.ToString();
                failButton.SetActive(false);
                ResultSelect();


            }
            else if (request.responseCode == 400)
            {

                Debug.Log("코인부족");
                succesButton.SetActive(false);
                failButton.SetActive(true);
            }
            else if (request.responseCode == 401)
            {

                Debug.Log("Unexpected Error");
            }
        }
    }

    



}
