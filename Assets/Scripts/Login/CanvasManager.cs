using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Login;
    public GameObject Register;
    public GameObject Popup;
    public TextMeshProUGUI typeError;
    public TextMeshProUGUI errorDescript;

    public enum mPageInfo
    {
        Login,
        Register
    }
    public enum errorInfo
    {
        passwordError,
        nonuserError,
        NetworkError,
        patternError,
        duplicateError,
        multipleError,
        success
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

    public void SetPopup(mPageInfo pageInfo, errorInfo error)
    {
        Popup.SetActive(true);

        if(error == errorInfo.success)
        {
            typeError.text = "회원가입 성공";
        }
        else if (pageInfo == mPageInfo.Login)
        {
            typeError.text = "로그인 실패";
        }
        else if(pageInfo == mPageInfo.Register)
        {
            typeError.text = "회원가입 실패";
        }

        
        if (error == errorInfo.NetworkError)
        {
            errorDescript.text = "네트워크 오류입니다";
        }
        else if (error == errorInfo.success)
        {
            errorDescript.text = "성공적으로 회원가입 되었습니다";
        }
        else if (error == errorInfo.passwordError)
        {
            errorDescript.text = "아이디 혹은 비밀번호\n오류입니다";
        }
        else if (error == errorInfo.nonuserError)
        {
            errorDescript.text = "존재하지 않는 계정입니다";
        }
        else if(error == errorInfo.patternError)
        {
            errorDescript.text = "비밀번호 형식이 잘못됐습니다\n비밀번호는 영문 숫자 특수기호 조합 8자리 이상\n 되어야 합니다";
        }
        else if(error == errorInfo.duplicateError)
        {
            errorDescript.text = "이미 존재하는 계정입니다";
        }
        else if (error == errorInfo.multipleError)
        {
            errorDescript.text = "이미 로그인중인 계정입니다";
        }

    }

    public void okClick()
    {
        Popup.SetActive(false);
    }
}