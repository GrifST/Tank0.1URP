using System;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
    [SerializeField] protected float SpeedRotation;

    [SerializeField] protected float SpeedTorward;
    [SerializeField] protected float HP;


    protected void RotationOnTarget(Vector2 target, float speed)
    {
        Vector2 lookDir = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        lookDir.Normalize();
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, speed * Time.deltaTime);
    }
}