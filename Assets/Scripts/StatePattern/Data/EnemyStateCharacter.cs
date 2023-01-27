using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

namespace StatePattern
{
    public class EnemyStateCharacter : MonoBehaviour
    {
        [Header("Initial Parametrs")] public float Eat = 1f;
        public float Energy = 1f;

        public State StartState;
        public State EatState;
        public State EnergyState;
        public State RandomMoveState;

        public Animator Animator;

        [Header("Actual State")] public State CurrentState;

        private float eatEndTime = 15f;
        private float EnergyEndTime = 15f;

        void Start()
        {
            SetState(StartState);
        }

        void Update()
        {
            Eat -= Time.deltaTime / eatEndTime;
            Energy -= Time.deltaTime / EnergyEndTime;

            if (!CurrentState.IsFinished)
            {
                CurrentState.Run();
            }
            else
            {
                if (Eat <= 0.4f)
                {
                    SetState(EatState);
                }
                else if (Energy <= 0.4f)
                {
                    SetState(EnergyState);
                }
                else
                {
                    SetState(RandomMoveState);
                }
            }
        }
        
        public void SetState(State state)
        {
            CurrentState = Instantiate(state);
            CurrentState.enemyStateCharacter = this;
            CurrentState.Init();
        }

        public void MoveTo(Vector3 position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(position - transform.position), Time.deltaTime * 120f);
        }
    }

}
