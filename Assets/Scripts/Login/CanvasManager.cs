using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Login;
    public GameObject Register;

    public enum mPageInfo
    {
        Login,
        Register
    }

    private mPageInfo CurrentPageInfo;
    public int CurrentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetCurrentPage(mPageInfo pageInfo)
    {
        CurrentPageInfo = pageInfo;
        CurrentPage = (int)CurrentPageInfo;

        Login.SetActive(false);
        Register.SetActive(false);

        if(CurrentPageInfo == mPageInfo.Login) { 
           Login.SetActive(true);  
        }
        else if(CurrentPageInfo == mPageInfo.Register) { 
            Register.SetActive(true); 
        }
    }
}
