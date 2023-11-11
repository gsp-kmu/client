using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ResponseSendDeck
{
    public int userId;
    public List<List<int>> deckList;
}
public class ButtonChangeScreen : MonoBehaviour
{
    public string url = "http://ec2-43-201-164-245.ap-northeast-2.compute.amazonaws.com:8000/savedeck";
    public string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange()
    {
        if (SceneManager.GetActiveScene().name == "MyCardScene")
        {
            CardDeck carddeck = GameObject.Find("CardDeckManger").GetComponent<CardDeck>();
            int id = 1;
            List<List<int>> ran = new List<List<int>>();
            ran = carddeck.allDecks;
            
            ResponseSendDeck deck = new ResponseSendDeck
            {
                userId = id,
                deckList = ran
            };

            string json = JsonConvert.SerializeObject(deck);
            Debug.Log("json");
            StartCoroutine(DeckPost(json));
            Debug.Log("코루틴끝");
            SceneManager.LoadScene(SceneToLoad);
        }
        else
        {

            SceneManager.LoadScene(SceneToLoad);
        }
        
    }

    IEnumerator DeckPost(string json)
    {
        Debug.Log("코루틴시작");

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json))
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
                Debug.Log("전부 성공");

                Debug.Log(responseJson);


            }
            else if (request.responseCode == 400)
            {
                Debug.Log("덱이 모잘라서 일부만 업데이트");
            }

            else if (request.responseCode == 401)
            {
                Debug.Log("실패");
            }

            else if (request.responseCode == 402)
            {
                Debug.Log("json오류");
            }
        }
    }


}
