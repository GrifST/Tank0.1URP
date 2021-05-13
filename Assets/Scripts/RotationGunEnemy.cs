using System;
using UnityEngine;

public class RotationGunEnemy : BaseTank
{
    [SerializeField] Shoting _shoting;
    [SerializeField] Transform target;
    [SerializeField] Transform firepoint;
    [SerializeField] private float angle = 360;
    public float timer = 0f;
    public float _rateFireoffspeed = 3f;
    private bool acessFire;
    [SerializeField] private float fireDistance=10f;

    void Start()
    {
        //   RaycastHit2D hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    private void Update()
    {
        // Debug.DrawRay(firepoint.position, transform.up, Color.green);
        // Debug.Log(target);

        if (target == null) return;
        base.RotationOnTarget(target.transform.position, SpeedTorward);
        GetAngleAttack();
    }

    private float GetDistance()
    {
        if (target == null || this.gameObject == null) return Mathf.Infinity;
        var distace = Vector2.Distance(transform.position, target.position);
        return distace;
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
                    if(GetDistance()>fireDistance) return;
                    _shoting.Shoot();
                }
            }
        }
    }
}