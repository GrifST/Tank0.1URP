using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefPlayer;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private Camcontrol cam;
    [SerializeField] private StatSetter _statSetter;
    [SerializeField] private int lives;
    [SerializeField] private Text livesUi;
    [SerializeField] private Text gameoverUI;
    [SerializeField] private Button restartButton;
    [SerializeField] private Croshire croshire;
    private void Start()
    {
        Cursor.visible = false;
        GoGoTank();
        gameoverUI.enabled = false;
        livesUi.text = lives.ToString();
        restartButton.gameObject.SetActive(false);
    }

    private void GoGoTank()
    {
        if (lives <= 0) return;
        PlayerCreate(prefPlayer).transform.position = spawnPlayer.position;
    }

    private GameObject PlayerCreate(GameObject pref)
    {
        var temp = Instantiate(pref);
        temp.GetComponentInChildren<HelthControl>().Setter = _statSetter;
        temp.GetComponentInChildren<HelthControl>().OnDead += OnPlayerDeda;
        cam.player = temp;
        return temp;
    }

    private void OnPlayerDeda(GameObject player)
    {   
        player.GetComponentInChildren<HelthControl>().OnDead -= OnPlayerDeda;
        Destroy(player);
        lives--;
        GameOver();
        GoGoTank();
    }
    private void GameOver()
    {
        livesUi.text = lives.ToString();
        
        if (lives <= 0)
        {
            gameoverUI.enabled = true;
            restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
            croshire.enabled = false;
        }
    }
    
}