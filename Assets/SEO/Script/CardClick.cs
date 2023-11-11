using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardClick : MonoBehaviour
        ,IPointerClickHandler
{
    CardDeck cardDeck;
    public int num;
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        CardDeck cardDeck = GameObject.Find("CardDeckManger").GetComponent<CardDeck>();
        string a = (eventData.pointerClick.tag.ToString());
        int n = Convert.ToInt32(a);
        //��ī�� �����ִ� ī�尡 ���õɶ�
        if (eventData.pointerClick.transform.parent == GameObject.FindGameObjectWithTag("AllCard").transform) {
            if (n >= 10)
            {
                a = a[1].ToString();
                GameObject cardin = GameObject.Find("card0/" + a);
                GameObject cardin2 = GameObject.Find("card1/" + a);

                // �ڽ��� ������(ī�尡 �ڸ��� ������)
                if (cardin.transform.childCount == 0)
                {
                    eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
                    eventData.pointerClick.transform.SetParent(cardin.transform);
                    eventData.pointerClick.transform.position = cardin.transform.position;
                    cardDeck.currentCardState[num] = 1;
                    cardDeck.currentDeck.Add(n + 1);

                }
                else if (cardin2.transform.childCount == 0)
                {
                    eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
                    eventData.pointerClick.transform.SetParent(cardin2.transform);
                    eventData.pointerClick.transform.position = cardin2.transform.position;
                    cardDeck.currentCardState[num] = 1;
                    cardDeck.currentDeck.Add(n + 1);
                }
            }
            else
            {
                GameObject cardin = GameObject.Find("card0/" + a);
                GameObject cardin2 = GameObject.Find("card1/" + a);

                // �ڽ��� ������(ī�尡 �ڸ��� ������)
                if (cardin.transform.childCount == 0)
                {
                    eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
                    eventData.pointerClick.transform.SetParent(cardin.transform);
                    eventData.pointerClick.transform.position = cardin.transform.position;
                    cardDeck.currentCardState[num] = 1;
                    cardDeck.currentDeck.Add(n + 1);

                }
                else if (cardin2.transform.childCount == 0)
                {
                    eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
                    eventData.pointerClick.transform.SetParent(cardin2.transform);
                    eventData.pointerClick.transform.position = cardin2.transform.position;
                    cardDeck.currentCardState[num] = 1;
                    cardDeck.currentDeck.Add(n + 1);
                }

            }
            
        }
        else
        {
            GameObject allcard = GameObject.FindGameObjectWithTag("AllCard");
            eventData.pointerClick.transform.SetParent(allcard.transform);
            eventData.pointerClick.transform.localScale = new Vector3(3f, 3f, 3f);
            cardDeck.currentCardState[num] = 0;
            cardDeck.currentDeck.Remove(n + 1);



        }


    }
}
