using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Ссылка на пушку")]
    [SerializeField] private Shoting _shoting; 

    public float speedMax = 250f;
    public float torqueMax = 100f;

    Rigidbody2D rigidBody;
    public Renderer wheelLeftRenderer;
    public Renderer wheelRightRenderer;
    private Material wheelLeft;
    private Material wheelRight;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        wheelLeft = new Material(wheelLeftRenderer.sharedMaterial);
        wheelLeftRenderer.sharedMaterial = wheelLeft;
        wheelRight = new Material(wheelRightRenderer.sharedMaterial);
        wheelRightRenderer.sharedMaterial = wheelRight;
    }

    float vertical = 0;
    float horizontal = 0;
    bool isDrift = false;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _shoting.Shoot();
        }

        vertical = Input.GetAxis("Vertical"); //вперед/назад
        horizontal = Input.GetAxis("Horizontal"); //лево/право
        if (isDrift) return; //Блокируем управление в заносе
        if (vertical != 0)
        {
            rigidBody.AddRelativeForce(Vector2.up * vertical * speedMax, ForceMode2D.Force);
        }

        wheelSpeedLeft = -vertical * 2f; //изначально скорость ставится прямо или назад
        wheelSpeedRight = -vertical * 2f; //на обе гусли
        if (horizontal != 0)
        {
            if (vertical < 0) rigidBody.angularVelocity += horizontal * torqueMax;
            else rigidBody.angularVelocity -= horizontal * torqueMax;
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

    float wheelSpeedLeft = 0;
    float wheelSpeedRight = 0;
    Vector2 wheelTilingLeft = new Vector2(0, 0);
    Vector2 wheelTilingRight = new Vector2(0, .5f);

    private void Update()
    {
        DriftControl();
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

    private void DriftControl()
    {
        if (Input.GetButtonDown("Jump") && !isDrift && vertical != 0)
        {
            isDrift = true;
            rigidBody.drag *= .24f;  //25f
            rigidBody.angularDrag *= .05f;  //05f
            wheelSpeedLeft = 0;
            wheelSpeedRight = 0;
        }
        else if (Input.GetButtonUp("Jump") && isDrift)
        {
            rigidBody.drag *= 4f;
            rigidBody.angularDrag *= 20f;
            isDrift = false;
        }
    }
}