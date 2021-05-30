using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private SpawnEnemyControler spawnEnemyControler;
    [SerializeField] private GameManager gameManager;
    private List<Bonus> bonuss = new List<Bonus>();

    private void Start()
    {    
        bonuss.AddRange(FindObjectsOfType<Bonus>());
    }
}