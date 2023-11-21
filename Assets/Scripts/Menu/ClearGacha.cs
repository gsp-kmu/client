using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ClearGacha : MonoBehaviour
{
    public RandomSelect randomSelect;
    private int mycoin;
    public GameObject coinText;
    private string getCoinUrl = GSP.http.getCoin;
    // Start is called before the first frame update
    public void Awake()
    {
        /// id 입력 받는곳 /////
        int id = 1;
        RequestSendCoin coinid = new RequestSendCoin
        {
            userId = id
        };

        string json = JsonUtility.ToJson(coinid);

        StartCoroutine(GetCoin(json));

    }
    IEnumerator GetCoin(string json)
    {

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(getCoinUrl, json))
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
                Debug.Log("코인 받아오기 성공");

                Debug.Log(responseJson);
                ResponseGetCoin response = JsonConvert.DeserializeObject<ResponseGetCoin>(responseJson);
                mycoin = response.coin;
                coinText.GetComponent<TextMeshProUGUI>().text = mycoin.ToString();

            }
            else if (request.responseCode == 400)
            {
                Debug.Log("받아오기 실패");
            }
        }
    }

    public void Destroygacha()
    {
        if(randomSelect != null)
        {
            //랜덤카드리스트 지우기
            randomSelect.result.Clear();

            //랜덤카드리스트ui지우기
            foreach(GameObject card in randomSelect.cardUIs)
            {
                card.GetComponent<CardUI>().DestoryCard();
            }

            randomSelect.cardUIs.Clear();

            //비활성화한 가챠버튼 다시 활성화
            randomSelect.gachaButton.GetComponent<Button>().enabled = true;
        }
    }

}
