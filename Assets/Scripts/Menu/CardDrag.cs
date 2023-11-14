using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
    , IDragHandler
    , IEndDragHandler
    ,IBeginDragHandler
{
    private Transform m_Parent;
    private Vector3 targetfirst;
    public void OnBeginDrag(PointerEventData eventData)
    {

        targetfirst = transform.position;
        m_Parent = transform.parent;
        transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform);
        GetComponent<Graphic>().raycastTarget = false;
    }



    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (transform.parent.CompareTag("AllCard"))
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else
        {
            transform.SetParent(m_Parent);
            transform.position = targetfirst;
            Debug.Log("else");
        }
        GetComponent<Graphic>().raycastTarget = true;
    }

    
}
