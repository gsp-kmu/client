using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTEEE : MonoBehaviour
{
    // Update is called once per frame

    void Start(){
        NetworkService.Instance.AddEvent(NetworkEvent.TEST_CARD, (Data.Card card)=>{
            Debug.Log("id: "+ card.id+ "  name: "+ card.name+ "  url: "+ card.url);
            Debug.Log("받음");
        });
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            Data.Card card;
            card.id = "absdiosnef";
            card.name = "러브러브러브";
            card.url = "./Love.png";
            NetworkService.Instance.Send(NetworkEvent.TEST_CARD, card);
        }
    }
}
