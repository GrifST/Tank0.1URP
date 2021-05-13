using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllBonus : MonoBehaviour
{
    [SerializeField] private SpawnEnemyControler SpawnEnemyControler;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnEnemyControler.bonusDestroy();
        Destroy(gameObject);
    }
  
}
