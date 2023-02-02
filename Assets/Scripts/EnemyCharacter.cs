using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public Transform target;
    private Rigidbody2D rigidBody;
    public float stopDistance = 10f;
    
    [Header("State")]
    public State StartState;
    public State idleState;
    public State PatrolState;
    public State AttackState;

    [Header("CurrentState")] 
    public State CurrentState;
    protected  override void Start()
    {
        SetState(StartState);
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
        }
    }
    private void Update()
    {
        if (!CurrentState.IsFinished)
        {
            CurrentState.Run();
        }
        else
        {
            if (target == null)
            {
                SetState(AttackState);
            }
            else if (target)
            {
                SetState(PatrolState);
            }
            else
            {
                SetState(idleState);
            }
        }
    }
    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.enemyCharacter = this;
        CurrentState.Init();
    }
    
    public void MoveTo()
    {
        
        
    }
    
    
    public override void Kill()
    {
        GameManager.main.OnEnemyDead(this);
        base.Kill();
        
    }
}
