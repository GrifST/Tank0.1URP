using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =  "State/Attack")]
public class AtackState : State
{
    public override void Init()
    {
        
    }

    public override void Run()
    {
        if (enemyCharacter.target)
        {
            if (enemyCharacter.tower)
            {
                enemyCharacter.tower.targetPosition = enemyCharacter.target.position;
                var angle = Vector3.Angle(enemyCharacter.tower.transform.up, enemyCharacter.target.position - enemyCharacter.tower.transform.position);
                if (angle < 10f)
                {
                    enemyCharacter.tower.Shot();
                }
            }
            if (enemyCharacter.tank)
            {
                Vector3 direction = enemyCharacter.target.position - enemyCharacter.transform.position;
                float angle = Vector2.SignedAngle(enemyCharacter.transform.up, direction);
                float distance = Vector3.Distance(enemyCharacter.target.position,enemyCharacter.transform.position);
                enemyCharacter.tank.vertical = (Mathf.Abs(angle) > 90f) ? 0 : Mathf.Clamp(distance - enemyCharacter.stopDistance ,-1,1);
                enemyCharacter.tank.horizontal = -Mathf.Clamp(angle / 90f, -1, 1);  
            }
        }
    }
}
