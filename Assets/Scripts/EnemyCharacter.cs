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
    public State Idle;
    public State PatrolState;

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
            if (target)
            {
                AtackTarget();
            }
            else if (target = null)
            {
                SetState(PatrolState);
            }
            else
            {
                SetState(Idle);
            }
        }
    }
    private void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.enemyCharacter = this;
        CurrentState.Init();
    }
    
    public void AtackTarget()
    {
        if (tower)
        {
            tower.targetPosition = target.position;
            var angle = Vector3.Angle(tower.transform.up, target.position - tower.transform.position);
            if (angle < 10f)
            {
                tower.Shot();
            }
        }
        if (tank)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Vector2.SignedAngle(transform.up, direction);
            float distance = Vector3.Distance(target.position,transform.position);
            tank.vertical = (Mathf.Abs(angle) > 90f) ? 0 : Mathf.Clamp(distance - stopDistance ,-1,1);
            tank.horizontal = -Mathf.Clamp(angle / 90f, -1, 1);  
        }
    }
    
    
    public override void Kill()
    {
        GameManager.main.OnEnemyDead(this);
        base.Kill();
        
    }
}
