using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    public float StopDist;
    public float RetreatDist;
    public Transform Player;
    private Rigidbody2D rb;

    [SerializeField] private float searchTimer;
    [SerializeField] private float setTimer;
    RotationGunEnemy rotationGun;
    [Header("Траки")]
    public Track trackLeft;
    public Track trackRight;

    

    void Start()
    {
        rotationGun = GetComponentInChildren<RotationGunEnemy>();
        rb = this.GetComponent<Rigidbody2D>();

        //тэги это прохо. ты можешь огрести кучу проблем просто из за неверного имени.
        //рекомендую использовать поиск по типу компонента

        //Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

   

    private void FixedUpdate()
    {
        var currentSpeed = rb.velocity.magnitude;
        print(currentSpeed);

        if (searchTimer < setTimer && Player == null)
        {
            searchTimer -= Time.deltaTime;
            Player = FindObjectOfType<RotationGunPlayer>().transform;
            rotationGun.SetTarget(Player);


        }
        searchTimer = setTimer;

        if (Player != null)
        {
            SearchPlayer();
        }

        if (currentSpeed > 0)
        {
            trackStart();
        }
        else
        {
            trackStop();
        }

 
        
    }

    void SearchPlayer()
    {

        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        var dist = Vector2.Distance(transform.position, Player.position);
        
        if (dist > StopDist)
        {
           float c_speed = Mathf.Clamp((dist - StopDist) * acceleration, -speed, speed);
            rb.velocity = direction.normalized * c_speed ;
        }
        else if (dist < RetreatDist)
        {
            float c_speed = Mathf.Clamp((dist - RetreatDist) * acceleration, -speed, speed);
            rb.velocity = direction.normalized * -c_speed ;
        }
        else
        {
            rb.velocity = Vector3.zero;// останавиваем танк
        }
    }
    void trackStart()
    {
        trackLeft.animator.SetBool("IsMoving", true);
        trackRight.animator.SetBool("IsMoving", true);
    }

    void trackStop()
    {
        trackLeft.animator.SetBool("IsMoving", false);
        trackRight.animator.SetBool("IsMoving", false);
    }
}
