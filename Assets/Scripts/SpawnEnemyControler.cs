using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyControler : MonoBehaviour
{

    [SerializeField] private GameObject prefEnemy;
    [SerializeField] private StatSetter _statSetterEnemy;
    [SerializeField] private int kills;
    [SerializeField] private Text killsUi;
    [SerializeField] private int tankSpawn;
    [SerializeField] private int tankRound;
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<SpawnPoint> tempSpawnPoint = new List<SpawnPoint>();
    [SerializeField] private List<Enemy> tempTankOnScene = new List<Enemy>();


    void Start()
    {
       
        killsUi.text = kills.ToString();
        spawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
        tempSpawnPoint.AddRange(spawnPoints);
        EnemyGo();

    }

   

    private void EnemyGo()
    {
        // Условие победы
        if (tankRound <= 0)
        {
            print("заработало");
            return;

        }


        
       // var  tankOnScene = FindObjectsOfType<Enemy>();
        

        if (tempTankOnScene.Count >= tankSpawn) return;

        var tankFactory = tankSpawn - tempTankOnScene.Count;


        tankRound -= tankFactory;


        if (tankRound < 0)
        {
            tankRound = 0;
        }
       

        
       

        for (int i = 0; i < tankFactory; i++)
        {
           
            if (tempSpawnPoint.Count > 0)
            {
                int rnd = Random.Range(0, tempSpawnPoint.Count);
                EnemyCreate(prefEnemy).transform.position = tempSpawnPoint[rnd].transform.position;
              
                tempSpawnPoint.RemoveAt(rnd);
            }
            else
            {
                tempSpawnPoint.AddRange(spawnPoints);
            }
        }
        
    }
    private GameObject EnemyCreate(GameObject pref)
    {
        var temp = Instantiate(pref);
        tempTankOnScene.Add(temp.GetComponent<Enemy>());
        temp.GetComponent<HelthControl>().Setter = _statSetterEnemy;
        temp.GetComponent<HelthControl>().OnDead += OnEnemyDead;
        return temp;
    }
    private void OnEnemyDead(GameObject enemy)
    {
        enemy.GetComponent<HelthControl>().OnDead -= OnEnemyDead;
        tempTankOnScene.Remove(enemy.GetComponent<Enemy>());
        kills++;
        killsUi.text = kills.ToString();
        Destroy(enemy);
        EnemyGo();
    }
}
