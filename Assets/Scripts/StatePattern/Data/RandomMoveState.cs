using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;

namespace StatePattern
{
    [CreateAssetMenu]
    public  class RandomMoveState : State
    {
        public float MaxDistance = 5f;

        private Vector3 randomPosition;

        public override void Init()
        {
            var randomed = new Vector3(Random.Range(-MaxDistance, MaxDistance), 0f,
                Random.Range(-MaxDistance, MaxDistance));
            randomPosition = enemyStateCharacter.transform.position + randomed;
        }

        public override void Run()
        {
            var distance = (randomPosition - enemyStateCharacter.transform.position).magnitude;
            if (distance > 0.5f)
            {
                enemyStateCharacter.MoveTo(randomPosition);
            }
            else
            {
                IsFinished = true;
            }
        }
    }

}