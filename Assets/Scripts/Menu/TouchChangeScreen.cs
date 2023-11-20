using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchChangeScreen : MonoBehaviour
{
    public GameObject loading;
    public string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            loading.SetActive(true);
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
