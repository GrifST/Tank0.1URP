using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyControler : MonoBehaviour
{

    [SerializeField] private GameObject prefEnemy;
    [SerializeField] private Transform spawnEnemy ;
    [SerializeField] private StatSetter _statSetterEnemy;
    [SerializeField] private int kills;
    [SerializeField] private Text killsUi;

  // [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    void Start()
    {
        EnemyGo();
        killsUi.text = kills.ToString();
       // spawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
    }


    private void EnemyGo()
    {
        EnemyCreate(prefEnemy).transform.position = spawnEnemy.transform.position;

    }
    private GameObject EnemyCreate(GameObject pref)
    {
        var temp = Instantiate(pref);
        temp.GetComponent<HelthControl>().Setter = _statSetterEnemy;
        temp.GetComponent<HelthControl>().OnDead += OnEnemyDead;
        return temp;
    }
    private void OnEnemyDead(GameObject Enemy)
    {
        Enemy.GetComponent<HelthControl>().OnDead -= OnEnemyDead;
        kills++;
        killsUi.text = kills.ToString();
        Destroy(Enemy);
        EnemyGo();
    }
}
