using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearGacha : MonoBehaviour
{
    public RandomSelect randomSelect;
    // Start is called before the first frame update
    void Start()
    {
        
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
            randomSelect.gachabutton.GetComponent<Button>().enabled = true;
        }
    }

}
