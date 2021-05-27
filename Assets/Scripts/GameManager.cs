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
    [SerializeField] private Text victoryUi;
    [SerializeField] private Button restartButton;
    [SerializeField] private Croshire croshire;
    [SerializeField] private SpawnEnemyControler SpawnEnemyControler;
    private static bool gameOver = false;
    public GameObject TempTankPlayer;

    private void Start()
    {
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
    public void LivesScore()
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
        var temp = Instantiate(pref);
        TempTankPlayer = temp;
        temp.GetComponentInChildren<HelthControl>().Setter = _statSetter;
        temp.GetComponentInChildren<HelthControl>().OnDead += OnPlayerDeda;
        cam.player = temp;
        SpawnEnemyControler.SetEnemysTargetAtack(temp);
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