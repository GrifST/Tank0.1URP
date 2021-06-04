using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public Transform target;
    private Rigidbody2D rigidBody;
    public float stopDistance = 10f;
    protected override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
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
                tank.vertical = (Mathf.Abs(angle) > 90f) ? 0 : Mathf.Clamp(distance - stopDistance ,0,1);//вперед/назад
                tank.horizontal = -Mathf.Clamp(angle / 90f, -1, 1);  //лево/право
            }
        }
        else
        {
            if (tower) tower.targetPosition = tower.transform.position;
            if (tank)
            {
                tank.vertical = 0;
                tank.horizontal = 0;
            }
        }
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
    public override void Kill()
    {
        GameManager.main.OnEnemyDead(this);
        base.Kill();
    }
}
