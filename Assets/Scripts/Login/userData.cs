using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userData : MonoBehaviour
{
    private userData instance = null;
    private int userId
    {
        get
        {
            return userId;
        }
        set
        {
            userId = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUserData(int userId)
    {
        this.userId = userId;   
    }
}
