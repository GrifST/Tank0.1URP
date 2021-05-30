using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    
    
    
    public float speedMax = 250f;
    public float torqueMax = 100f;

    Rigidbody2D rigidBody;

    [Header("Траки")]
    public Renderer wheelLeftRenderer;
    public Renderer wheelRightRenderer;
    private Material wheelLeft;
    private Material wheelRight;
    float wheelSpeedLeft = 0;
    float wheelSpeedRight = 0;
    Vector2 wheelTilingLeft = new Vector2(0, 0);
    Vector2 wheelTilingRight = new Vector2(0, .5f);
    public float vertical;
    public float horizontal;
    private void Start()
    {
       
        rigidBody = GetComponent<Rigidbody2D>();
        wheelLeft = new Material(wheelLeftRenderer.sharedMaterial);
        wheelLeftRenderer.sharedMaterial = wheelLeft;
        wheelRight = new Material(wheelRightRenderer.sharedMaterial);
        wheelRightRenderer.sharedMaterial = wheelRight;
    }
    private void FixedUpdate()
    {
        wheelSpeedLeft = -vertical * 2f; //изначально скорость ставится прямо или назад
        wheelSpeedRight = -vertical * 2f; //на обе гусли
        if (horizontal != 0)
        {

            if (vertical != 0)
            {
                //затем если едем
                if (horizontal > 0) wheelSpeedRight *=  (1.3f - horizontal); //то замедляем или останавливаем (по желанию) гуслю в стороне поворота
                else wheelSpeedLeft *=  (1.3f + horizontal);
            }
            else
            {
                // иначе если стоим, то вертим гусли против друг друга
                wheelSpeedLeft -= horizontal;
                wheelSpeedRight += horizontal;
            }
        }
        rigidBody.velocity = transform.up * vertical * speedMax;
        rigidBody.angularVelocity = (vertical < 0 ? horizontal : -horizontal) * torqueMax;

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
}