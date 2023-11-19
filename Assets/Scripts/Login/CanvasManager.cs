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
        multipleError
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

        if (pageInfo == mPageInfo.Login)
        {
            typeError.text = "�α��� ����";
        }
        else if(pageInfo == mPageInfo.Register)
        {
            typeError.text = "ȸ������ ����";
        }

        
        if (error == errorInfo.NetworkError)
        {
            errorDescript.text = "��Ʈ��ũ �����Դϴ�";
        }
        else if (error == errorInfo.passwordError)
        {
            errorDescript.text = "���̵� Ȥ�� ��й�ȣ\n�����Դϴ�";
        }
        else if (error == errorInfo.nonuserError)
        {
            errorDescript.text = "�������� �ʴ� �����Դϴ�";
        }
        else if(error == errorInfo.patternError)
        {
            errorDescript.text = "��й�ȣ ������ �߸��ƽ��ϴ�\n��й�ȣ�� ���� ���� Ư����ȣ ���� 8�ڸ� �̻�\n �Ǿ�� �մϴ�";
        }
        else if(error == errorInfo.duplicateError)
        {
            errorDescript.text = "�̹� �����ϴ� �����Դϴ�";
        }
        else if (error == errorInfo.multipleError)
        {
            errorDescript.text = "�̹� �α������� �����Դϴ�";
        }

    }

    public void okClick()
    {
        Popup.SetActive(false);
    }
}
