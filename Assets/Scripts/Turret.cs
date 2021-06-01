using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speedRotation = 100f;
    public float accelRotation = 100f;
    private Gun[] guns;
    [SerializeField] private bool isPermanentShot;
    private float fireRateDelay;
    private float fireRateTimer;
    private int fireRateIndex = -1;
    void Start()
    {
        guns = GetComponentsInChildren<Gun>();
        fireRateDelay = 60f / guns[0].fireRate / guns.Length + 0.01f;
    }
    public void Shot()
    {
        if (isPermanentShot || guns.Length == 1)
        {
            foreach (var gun in guns)
            {
                gun.Shot();
            }
        }
        else
        {
          var newIndex = Mathf.FloorToInt(fireRateTimer / fireRateDelay);
            if (newIndex > fireRateIndex)
            {
                
                if (newIndex >= guns.Length)
                {
                    fireRateTimer = 0;
                    newIndex = 0;
                }
                fireRateIndex = newIndex;
                guns[fireRateIndex].Shot();
            }
            fireRateTimer += Time.deltaTime;
        }
        
    }
    
    void Update()
    {
        
        Vector3 direction = targetPosition - transform.position;
        float angle = Vector2.SignedAngle(transform.up, direction);
        angle = Mathf.Clamp(angle * accelRotation, -speedRotation, speedRotation);
        transform.Rotate(0,0,angle* Time.deltaTime);
    }

    
   
}
