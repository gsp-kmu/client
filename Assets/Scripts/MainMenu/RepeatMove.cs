using UnityEngine;
using DG.Tweening;

public class RepeatMove : MonoBehaviour
{
    public float moveDistance;
    public float duration;

    private void Start()
    {
  
        StartAnimation();
    }

    void StartAnimation()
    {
        transform.DOMoveX(transform.position.x + moveDistance, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                StartAnimation();
            })
            .SetLoops(-1, LoopType.Yoyo); 
    }
}