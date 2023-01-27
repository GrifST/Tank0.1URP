using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    [CreateAssetMenu]
    public abstract class State : ScriptableObject
    { 
        public bool IsFinished { get; protected set; }
        [HideInInspector] public EnemyStateCharacter enemyStateCharacter;
        
        public virtual void Init(){}
        public abstract void Run();
    }

}
