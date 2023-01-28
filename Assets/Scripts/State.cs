using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu]
    public abstract class State : ScriptableObject
    { 
        public bool IsFinished { get; protected set; }
        [HideInInspector] public EnemyCharacter enemyCharacter;
        
        public virtual void Init(){}
        public abstract void Run();
    }

