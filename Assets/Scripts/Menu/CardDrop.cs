using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDrop : MonoBehaviour,IDropHandler
{   

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name == "Image"){
            Debug.Log("drop");
            //Ŭ������ ��ü�Ǿ� ��� ���ҵ�,
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.position = this.transform.position;
        }

    }
    
}
