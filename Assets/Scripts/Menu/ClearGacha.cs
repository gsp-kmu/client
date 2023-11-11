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
            //����ī�帮��Ʈ �����
            randomSelect.result.Clear();

            //����ī�帮��Ʈui�����
            foreach(GameObject card in randomSelect.cardUIs)
            {
                card.GetComponent<CardUI>().DestoryCard();
            }

            randomSelect.cardUIs.Clear();

            //��Ȱ��ȭ�� ��í��ư �ٽ� Ȱ��ȭ
            randomSelect.gachabutton.GetComponent<Button>().enabled = true;
        }
    }

}
