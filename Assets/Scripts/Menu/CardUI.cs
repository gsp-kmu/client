using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Data;
using DG.Tweening;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image chr;
    public int cardId;
    public GameObject sameText;
    public GameObject gachaParticle;
    public GameObject gachaParticlebg;
    // id 에서 -1한 값을 이용함
    public List<int> rareCarList = new List<int>{10, 14, 16, 19};
    Animator animator;
    private void Start()
    {
        sameText.SetActive(false);
        animator = GetComponent<Animator>();
        
        animator.enabled = false;
        //this.transform.DOShakeScale(5.0f, 0.1f, 10, 90);
    }
    // 카드의 정보를 초기화
    public void CardUISet(Card_ card)
    {
        chr.sprite = card.cardImage;
        cardId = card.cardId;
        if (cardId == 10 || cardId == 14 || cardId == 16 || cardId ==19)
        {
            gachaParticlebg.SetActive(true);
        }
    }
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {

        transform.DOShakePosition(0.8f, new Vector3(10.0f, 10.3f, 0f), 10, 90).OnComplete(() =>
        {

            animator.enabled = true;
            // 흔들리고 나서 Flip 애니메이션 실행
            animator.SetTrigger("Flip");
            if(cardId == 10 || cardId == 14 || cardId == 16 || cardId == 19)
            {
                gachaParticle.SetActive(true);
            }

            this.transform.DOComplete();
            sameText.SetActive(true);
        });
    }

    //생성된 카드 ui 삭제
    public void DestoryCard()
    {
        Destroy(gameObject);
    }
}
