using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTEEE : MonoBehaviour
{
    // Update is called once per frame

    void Start(){
        NetworkService.Instance.AddEvent(NetworkEvent.TEST_CARD, (Data.Card card)=>{
            Debug.Log("id: "+ card.id);
            Debug.Log("받음");
        });
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            Data.Card card;
            card.id = 1;
            NetworkService.Instance.Send(NetworkEvent.TEST_CARD, card);
        }
    }
}
