using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero : Card
{
    void Start()
    {
        num = 0;
    }

    void Update()
    {
        
    }

    public override void BattleCry(Digit d)
    {
        Debug.Log("zero");

        
    }
}
