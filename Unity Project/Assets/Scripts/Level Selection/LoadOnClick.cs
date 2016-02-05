using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    /*[HideInInspector]*/ public GameObject loadingScreen;

    public void Start()
    {
        if (!loadingScreen)
        {
            loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        }
    }

    /*public void OnTouchDown()
    { 
        LoadScene(SceneToLoad);
    }*/

    public void LoadScene(int level)
    {
        loadingScreen.SetActive(true);
        Application.LoadLevel(level);
    }

}
