using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleFieldCards : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void OrganizeCard()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i * 2 + 1000;
        }
    }
}
