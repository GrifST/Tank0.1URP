using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private SpawnEnemyControler spawnEnemyControler;
    [SerializeField] private GameManager gameManager;
    private List<Bonus> bonuss = new List<Bonus>();

    private void Start()
    {
        var bonussTemp = FindObjectsOfType<Bonus>();
        bonuss.AddRange(bonussTemp);
        foreach (var bonus in bonuss)
        {
            bonus.MeGet += GetBonus;
        }


        void GetBonus(Bonus bonus, TypeBonus obj)
        {
            var tempHealhT = gameManager.TempTankPlayer.GetComponent<HelthControl>();
            // обработка
            switch (obj)
            {
                case TypeBonus.destroyAll:
                    spawnEnemyControler.AllTankDestroy();
                    break;
                case TypeBonus.restorHealth:
                    tempHealhT.ResetHelthPoint();
                    break;
                case TypeBonus.restorShield:
                    tempHealhT.ResetShielPoint();
                    break;
                case TypeBonus.extraLife:
                    gameManager.LivesScore();
                    break;
            }

            //удаление
            bonus.MeGet -= GetBonus;
            Destroy(bonus.gameObject);
        }
    }
}