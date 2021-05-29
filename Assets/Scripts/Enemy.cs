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
    private Rigidbody2D rigidBody;

    [SerializeField] private float searchTimer;
    [SerializeField] private float setTimer;
    RotationGunEnemy rotationGun;
    [Header("Траки")]
    public Renderer wheelLeftRenderer;
    public Renderer wheelRightRenderer;
    private Material wheelLeft;
    private Material wheelRight;
    float wheelSpeedLeft = 0;
    float wheelSpeedRight = 0;
    Vector2 wheelTilingLeft = new Vector2(0, 0);
    Vector2 wheelTilingRight = new Vector2(0, .5f);
    public float speedMax = 250f;
    public float torqueMax = 100f;

    void Start()
    {
        rotationGun = GetComponentInChildren<RotationGunEnemy>();
        rigidBody = this.GetComponent<Rigidbody2D>();
        wheelLeft = new Material(wheelLeftRenderer.sharedMaterial);
        wheelLeftRenderer.sharedMaterial = wheelLeft;
        wheelRight = new Material(wheelRightRenderer.sharedMaterial);
        wheelRightRenderer.sharedMaterial = wheelRight;
        //тэги это прохо. ты можешь огрести кучу проблем просто из за неверного имени.
        //рекомендую использовать поиск по типу компонента

        //Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

   

    private void FixedUpdate()
    {
        var currentSpeed = rigidBody.velocity.magnitude;
       

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
        float dir = Vector3.Angle(rigidBody.velocity, transform.up);
        float vertical = (dir > 0.1f) ? -rigidBody.velocity.magnitude : rigidBody.velocity.magnitude ;//вперед/назад
        float horizontal = rigidBody.angularVelocity / 100; //лево/право
        wheelSpeedLeft = -vertical * 2f; //изначально скорость ставится прямо или назад
        wheelSpeedRight = -vertical * 2f; //на обе гусли
        if (horizontal != 0)
        {
           
            if (vertical != 0)
            {
                //затем если едем
                if (horizontal > 0)
                    wheelSpeedRight *= .3f; //то замедляем или останавливаем (по желанию) гуслю в стороне поворота
                else wheelSpeedLeft *= .3f;
            }
            else
            {
                // иначе если стоим, то вертим гусли против друг друга
                wheelSpeedLeft += horizontal;
                wheelSpeedRight -= horizontal;
            }
        }

    }
    private void LateUpdate()
    {

        UpdateWheelTiling(wheelLeft, ref wheelTilingLeft, wheelSpeedLeft);
        UpdateWheelTiling(wheelRight, ref wheelTilingRight, wheelSpeedRight);
    }

    private void UpdateWheelTiling(Material wheelMaterial, ref Vector2 tiling, float speed)
    {
        tiling.y += speed * Time.deltaTime;
        if (Mathf.Abs(tiling.y) >= 1f) tiling.y = 0;
        wheelMaterial.SetTextureOffset("_MainTex", tiling);
    }
    public void SetTarget(GameObject player)
    {
        this.Player = player.transform;
        rotationGun.SetTarget(this.Player);
    }
    void SearchPlayer()
    {

        Vector3 direction = Player.position - transform.position;
        float angle = Vector2.SignedAngle(transform.up, direction);
        
        angle = Mathf.Clamp(angle, -100, 100);
        rigidBody.angularVelocity = angle;
        

        var dist = Vector2.Distance(transform.position, Player.position);
        
        if (dist > StopDist)
        {
           float c_speed = Mathf.Clamp((dist - StopDist) * acceleration, -speed, speed);
            rigidBody.velocity = transform.up * c_speed;
            
        }
        else if (dist < RetreatDist)
        {
            float c_speed = Mathf.Clamp((RetreatDist - dist) * acceleration, -speed, speed);
            rigidBody.velocity = transform.up * -c_speed ;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;// останавиваем танк
        }
    }
   
}
