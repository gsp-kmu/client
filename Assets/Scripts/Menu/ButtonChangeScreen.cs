using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ButtonChangeScreen : MonoBehaviour
{
    public CardDeck carddeck;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("hi");
            ButtonClick.instance.PlayButtonClick();
        }
    }
    public void ClickMyCardScene()
    {
        Debug.Log("hi");
        ButtonClick.instance.PlayButtonClick();
        SceneManager.LoadScene(GSP.Scene.mycard);
    }
    public void ClickMainMenu()
    {
        ButtonClick.instance.PlayButtonClick();
        SceneManager.LoadScene(GSP.Scene.main);
    }
    public void ClickCardGarcha()
    {
        ButtonClick.instance.PlayButtonClick();
        SceneManager.LoadScene(GSP.Scene.garcha);
    }


}
