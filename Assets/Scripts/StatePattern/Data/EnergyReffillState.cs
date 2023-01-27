using UnityEngine;

namespace StatePattern
{
    [CreateAssetMenu]
    public class EnergyReffillState : State
    {
        private Transform targetBed;

        private Vector3 lastCharPos;

        private bool isSleepStarted;
        private float sleepTimeLeft = 7f;


        public override void Init()
        {
            targetBed = GameObject.FindGameObjectWithTag("Bed").transform;
        }

        public override void Run()
        {
            if (IsFinished) return;

                if (!isSleepStarted)
                {
                    MoveToBed();
                }
                else
                {
                    DoSleep();
                }
        }

        

        private void MoveToBed()
        {
            var distance = (targetBed.position - enemyStateCharacter.transform.position).magnitude;

            if (distance >0.2f)
            {
                enemyStateCharacter.MoveTo(targetBed.position);
            }
            else
            {
                lastCharPos = enemyStateCharacter.transform.position;
                enemyStateCharacter.transform.position = targetBed.position;
                
                enemyStateCharacter.Animator.Play("Sleep");
                isSleepStarted = true;
            }
        }
        
        private void DoSleep()
        {
            sleepTimeLeft -= Time.deltaTime;

            if (sleepTimeLeft > 0) return;
            
            enemyStateCharacter.Animator.Play("EndSLeep");
            enemyStateCharacter.transform.position = lastCharPos;
            enemyStateCharacter.Energy = 1f;
            IsFinished = true;
        }
    }
}
