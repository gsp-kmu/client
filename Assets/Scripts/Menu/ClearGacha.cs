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
        /// id �Է� �޴°� /////
        int id = int.Parse(NetworkService.Instance.id);
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
                Debug.Log("���� �޾ƿ��� ����");

                Debug.Log(responseJson);
                ResponseGetCoin response = JsonConvert.DeserializeObject<ResponseGetCoin>(responseJson);
                mycoin = response.coin;
                coinText.GetComponent<TextMeshProUGUI>().text = mycoin.ToString();

            }
            else if (request.responseCode == 400)
            {
                Debug.Log("�޾ƿ��� ����");
            }
        }
    }

    public void Destroygacha()
    {
        if(randomSelect != null)
        {
            //����ī�帮��Ʈ �����
            randomSelect.result.Clear();

            //����ī�帮��Ʈui�����
            foreach(GameObject card in randomSelect.cardUIs)
            {
                card.GetComponent<CardUI>().DestoryCard();
            }

            randomSelect.cardUIs.Clear();

            //��Ȱ��ȭ�� ��í��ư �ٽ� Ȱ��ȭ
            randomSelect.gachaButton.GetComponent<Button>().enabled = true;
        }
    }

}
