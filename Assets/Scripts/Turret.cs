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
    void Start()
    {
        guns = GetComponentsInChildren<Gun>();
    }
    public void Shot()
    {
        foreach(var gun in guns)
        {
            gun.Shot();
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
