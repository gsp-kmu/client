using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum State
    {
        Hand,
        Select,
        Battlefield
    };

    public enum Digit{
        One,
        Ten
    }
    State state;

    public int num;
    public Digit digit;
    
    void Start() {
    
    }
    
    void Update() {

    }

    public virtual void BattleCry(Digit digit)
    {
        Debug.Log(transform.name + " 능력발동");
    }

    public virtual void BattleCryOpponent(Digit digit)
    {
        Debug.Log("상대방 " + transform.name + " 능력발동");
    }
}
