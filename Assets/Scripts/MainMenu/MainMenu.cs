using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GSP.Hs
{
    public class MainMenu : MonoBehaviour
    {
        public SceneCardTrans sceneCardTrans;
        public MatchingController matchingController;

        private void Start()
        {
            matchingController.InitCallbackMathicngEnd(GameStart);
            NetworkService.Instance.AddEvent(NetworkEvent.MATCH_SUCCESS, (string data) =>
            {
                matchingController.onMatchingSuccess();
            });
            NetworkService.Instance.AddEvent(NetworkEvent.MATCH_END, (string data) =>
            {
                matchingController.onMatchingEnd();
            });
        }

        private void OnDestroy()
        {
            NetworkService.Instance.RemoveEvent(NetworkEvent.MATCH_SUCCESS);
            NetworkService.Instance.RemoveEvent(NetworkEvent.MATCH_END);
        }

        public void GameStart()
        {
            sceneCardTrans.FadeIn();
            Invoke("MoveScene", 0.2f);
        }

        private void MoveScene()
        {
            Debug.Log("GaemStart");
            SceneManager.LoadScene(GSP.Scene.ingame);
        }
    }

}
