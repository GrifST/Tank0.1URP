using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject loader;
    [SerializeField] private Button startScreenUi;
    [SerializeField] private Button exitScreenUi;
    [SerializeField] private string nameScene;
    
    public void LoadGame()
    {
        StartCoroutine(LoadYourAsyncScene(nameScene));
        if (startScreenUi != null) startScreenUi.interactable = false;
        if (exitScreenUi != null) exitScreenUi.interactable = false;
        loader.SetActive(true);
    }
    
    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene(nameScene));
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

}