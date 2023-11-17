using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public Transform animation1;
    public Transform animation2;
    public Vector3 move1;
    public Vector3 move2;
    public float moveTime1;
    public float moveTime2;
    public Vector3 rotate1;
    public Vector3 rotate2;
    public float rotateTime1;
    public float rotateTime2;
    // Start is called before the first frame update
    void Start()
    {
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
        animation1.DOLocalMove(move1, moveTime1);
        animation2.DOLocalMove(move2, moveTime2);
    
    }
}
