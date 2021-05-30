using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllBonus : Bonus
{
    protected override void OnTake(Character character)
    {
        
        var enemyList = SpawnEnemyControler.main.enemyTankOnScene;
        foreach (var enemy in enemyList.ToArray())
        {
            enemy.Kill();
        }

        base.OnTake(character);

    }

}
