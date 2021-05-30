using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnEnemyControler : MonoBehaviour
{
    public static SpawnEnemyControler main;
    [SerializeField] private GameObject prefEnemy;
    [SerializeField] private int tankSpawn;
    [SerializeField] private int tankRound;
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<SpawnPoint> tempSpawnPoint = new List<SpawnPoint>();
    public List<EnemyCharacter> enemyTankOnScene = new List<EnemyCharacter>();

    private void Awake()
    {
        main = this;
    }
    void Start()
    {
        spawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
        tempSpawnPoint.AddRange(spawnPoints);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // Условие победы
        if (tankRound <= 0)
        {
            if (enemyTankOnScene.Count == 0) GameManager.main.VictoryBatlle();
            return;
        }

        if (enemyTankOnScene.Count >= tankSpawn) return;

        var tankFactory = tankSpawn - enemyTankOnScene.Count;

        tankRound -= tankFactory;

        if (tankRound < 0)
        {
            tankRound = 0;
        }

        for (int i = 0; i < tankFactory; i++)
        {
            if (tempSpawnPoint.Count == 0) tempSpawnPoint.AddRange(spawnPoints);

            int rnd = Random.Range(0, tempSpawnPoint.Count - 1);
            EnemyCreate(prefEnemy).transform.position = tempSpawnPoint[rnd].transform.position;

            tempSpawnPoint.RemoveAt(rnd);
        }
    }

    private GameObject EnemyCreate(GameObject pref)
    {
        var temp = Instantiate(pref);
        enemyTankOnScene.Add(temp.GetComponent<EnemyCharacter>());
        return temp;
    }
    public void EnemyRemove(EnemyCharacter enemy)
    {
        enemyTankOnScene.Remove(enemy);
    }
}