using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    [SerializeField] private GameObject prefPlayer;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private Camcontrol cam;
    [SerializeField] private int lives;
    [SerializeField] private Text livesUi;
    [SerializeField] private Text gameoverUI;
    [SerializeField] private Text victoryUi;
    [SerializeField] private int kills;
    [SerializeField] private Text killsUi;
    [SerializeField] private Button restartButton;
    [SerializeField] private Croshire croshire;
    private static bool gameOver = false;
    [SerializeField] private StatSetter statSetterPref;
    [SerializeField] private Transform statSetterContainer;
    


    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        killsUi.text = kills.ToString();
        Cursor.visible = false;
        GoGoTank();
        victoryUi.enabled = false;
        gameoverUI.enabled = false;
        livesUi.text = lives.ToString();
        restartButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameOver)
        {
            Cursor.visible = true;
            croshire.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            croshire.enabled = true;
        }
    }
    public StatSetter CreateStatSetter()
    {
        return Instantiate(statSetterPref, statSetterContainer);
    }
    public void AddPlayerLive()
    {
        lives++;
        livesUi.text = lives.ToString();
    }
    
    private void GoGoTank()
    {
        if (lives <= 0) return;
        PlayerCreate(prefPlayer).transform.position = spawnPlayer.position;
    }

    private GameObject PlayerCreate(GameObject pref)
    {
        var player = Instantiate(pref);
        cam.player = player;
        return player;
        

    }

    public void OnPlayerDead(PlayerCharecter player)
    {   
        lives--;
        GameOver();
        GoGoTank();
    }
    public void OnEnemyDead(EnemyCharacter enemy)
    {
        SpawnEnemyControler.main.EnemyRemove(enemy);
        kills++;
        killsUi.text = kills.ToString();
        SpawnEnemyControler.main.SpawnEnemy();
    }
    public void VictoryBatlle()
    {
        victoryUi.enabled = true;
        restartButton.gameObject.SetActive(true);
        Cursor.visible = true;
        croshire.enabled = false;
    }
    private void GameOver()
    {
        livesUi.text = lives.ToString();
        
        if (lives <= 0)
        {
            gameoverUI.enabled = true;
            gameOver = true;
            restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
            croshire.enabled = false;
           
        }
    }
    
}