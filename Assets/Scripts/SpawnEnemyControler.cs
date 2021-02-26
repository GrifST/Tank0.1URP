using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyControler : MonoBehaviour
{

    [SerializeField] private GameObject prefEnemy;
    [SerializeField] private Transform spawnEnemy;
    [SerializeField] private StatSetter _statSetterEnemy;

    void Start()
    {
        EnemyGo();
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
        Destroy(Enemy);
        EnemyGo();
    }
}
