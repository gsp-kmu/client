using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardClick : MonoBehaviour
        ,IPointerClickHandler
{
    public GameObject[] carddeck;
    // Start is called before the first frame update
public void OnPointerClick(PointerEventData eventData)
    {
        //내카드 위에있는 카드가 선택될때
        if (eventData.pointerClick.transform.parent == GameObject.FindGameObjectWithTag("AllCard").transform) { 
            carddeck = GameObject.FindGameObjectsWithTag("CardDeck");
            string a = (eventData.pointerClick.tag.ToString());
            int n = Convert.ToInt32(a);
            GameObject cardin = GameObject.Find("card" + a);
            GameObject cardin2 = GameObject.Find("card1" + a);

            // 자식이 없을때(카드가 자리에 없을때)
            if (cardin.transform.childCount == 0) {
            eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
            eventData.pointerClick.transform.SetParent(cardin.transform);
            eventData.pointerClick.transform.position = cardin.transform.position;
        }
        else if(cardin2.transform.childCount == 0)
        {
            eventData.pointerClick.transform.localScale = new Vector3(2f, 2f, 2f);
            eventData.pointerClick.transform.SetParent(cardin2.transform);
            eventData.pointerClick.transform.position = cardin2.transform.position;
        }
        }
        else
        {
            GameObject allcard = GameObject.FindGameObjectWithTag("AllCard");
            eventData.pointerClick.transform.SetParent(allcard.transform);
            eventData.pointerClick.transform.localScale = new Vector3(3f, 3f, 3f);
        }


    }
}
