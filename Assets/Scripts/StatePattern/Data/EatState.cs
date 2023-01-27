using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;

namespace StatePattern
{
    [CreateAssetMenu]
    public  class EatState : State
    {
        public float RestoresEat = 0.6f;
        public State NoApplesState;

        private Transform targetApple;

        public override void Init()
        {
            var apples = GameObject.FindGameObjectsWithTag("Apple");
            if (apples.Length == 0)
            {
                enemyStateCharacter.SetState(NoApplesState);
                return;
            }

            targetApple = apples[Random.Range(0, apples.Length)].transform;
        }

        public override void Run()
        {
            if (IsFinished)
            {
                return;
            }

            MoveToApple();
        }

        private void MoveToApple()
        {
            var distance = (targetApple.position - enemyStateCharacter.transform.position).magnitude;

            if (distance > 1f)
            {
                enemyStateCharacter.MoveTo(targetApple.position);
            }
            else
            {
                Destroy(targetApple.gameObject);
                enemyStateCharacter.Eat += RestoresEat;
                IsFinished = true;
            }
        }
    }

}
