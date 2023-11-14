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
            //클릭으로 대체되어 사용 안할듯,
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.position = this.transform.position;
        }

    }
    
}
