using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public RectTransform animation1;
    public RectTransform animation2;
    public Vector3 move1;
    public Vector3 move2;
    public float moveTime1;
    public float moveTime2;
    public Vector3 rotate1;
    public Vector3 rotate2;
    public float rotateTime1;
    public float rotateTime2;

    public Image characterImage1;
    public Image characterImage2;
    public Image background;

    public Sprite[] characterSprites1;
    public Sprite[] characterSprites2;
    public Sprite[] backgroundSprites;

    // Start is called before the first frame update
    void Start()
    {
        int idx1 = Random.Range(0, characterSprites1.Length);
        int idx2 = Random.Range(0, characterSprites2.Length);
        Sprite randomCharacter1 = characterSprites1[idx1];
        Sprite randomCharacter2 = characterSprites2[idx2];
        Sprite randomBackground = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        characterImage1.sprite = randomCharacter1;
        characterImage2.sprite = randomCharacter2;
        background.sprite = randomBackground;

        if(idx1 == 1 || idx1 == 2)
        {
            characterImage1.transform.localScale = new Vector3((float)7.622005, (float)9.087746, (float)3.709587);
        }
        

        StartCoroutine(Moveit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Moveit()
    {
        yield return new WaitForSeconds(1);
        animation1.DORotate(rotate1, rotateTime1);
        animation2.DORotate(rotate2, rotateTime2);
        animation1.DOAnchorPos3D(move1, moveTime1);
        animation2.DOAnchorPos3D(move2, moveTime2);
    
    }
}
