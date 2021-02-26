using System;
using UnityEngine;

public class RotationGunEnemy : BaseTank
{

    [SerializeField] Shoting _shoting;
    [SerializeField] Transform target;
    [SerializeField] private float angle = 360;
    public float timer = 0f;
    public float _rateFireoffspeed = 3f;
    private bool acessFire;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    private void Update()
    {


        if (target == null) return;
        base.RotationOnTarget(target.transform.position, SpeedTorward);
        GetAngleAttack();


    }

    private void GetAngleAttack()
    {
        //логика наведения
        //если угол = угол атаки то 
        //
        if (target == null) return;
        if (Vector3.Angle(transform.forward, target.position - transform.position) <= angle)
        {



            timer += Time.deltaTime;
            if (timer >= _rateFireoffspeed)
            {
                timer = 0;
                acessFire = true;

                if (acessFire)
                {

                    _shoting.Shoot();

                }
            }

        }

    }

}