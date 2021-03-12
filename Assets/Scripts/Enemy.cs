using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
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


    void Update()
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

    private void SearchPlayer()
    {

        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        var dist = Vector2.Distance(transform.position, Player.position);

        if (dist > StopDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else if (dist < StopDist && (dist > RetreatDist))
        {
            transform.position = Player.position;
        }
        else if (dist < RetreatDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
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
