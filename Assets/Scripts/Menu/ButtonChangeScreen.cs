using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ButtonChangeScreen : MonoBehaviour
{
    public CardDeck carddeck;
    public void ClickMyCardScene()
    {
        Debug.Log("hi");
        SceneManager.LoadScene(GSP.Scene.mycard);
    }
    public void ClickMainMenu()
    {
        SceneManager.LoadScene(GSP.Scene.main);
    }
    public void ClickCardGarcha()
    {
        SceneManager.LoadScene(GSP.Scene.garcha);
    }


}
