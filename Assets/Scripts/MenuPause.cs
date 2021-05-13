using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPause = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Croshire croshire;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();   
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        Cursor.visible = false;
        croshire.enabled = true;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        croshire.enabled = false;
        Cursor.visible = true;
        
    }


    public void exitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
    
}
