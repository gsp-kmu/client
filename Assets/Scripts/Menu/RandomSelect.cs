using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RandomSelect : MonoBehaviour
{
    public List<Card_> deck = new List<Card_>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합
    public GameObject gachabutton;
    public List<Vector3> cardpo = new List<Vector3>(); //카드 위치

    public void Start()
    {
        gachabutton.GetComponent<Button>().enabled = false;
        for (int i = 0; i < deck.Count; i++)
        {
            //가중치 총합 계산
            total += deck[i].weight;
        }
        ResultSelect();
    }
    public List<Card_> result = new List<Card_>();  // 랜덤카드 리스트

    public Transform parent;
    public GameObject cardprefab;
    public List<GameObject> cardUIs = new List<GameObject>();


    public void ResultSelect()
    {
        for (int i = 0; i < 5; i++)
        {
            //가중치계산으로 구한 카드를 리스트에 넣어줌
            result.Add(RandomCard()); 
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
    public Card_ RandomCard()
    {
        int weight = 0;
        int selectNum = 0;
        //여기서 임의의 숫자 받아오면 될듯

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if (selectNum <= weight)
            {
                Card_ temp = new Card_(deck[i]);
                return temp;
            }
        }
        return null;
    }



}
