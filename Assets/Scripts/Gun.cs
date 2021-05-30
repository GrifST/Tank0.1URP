using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectiles projectile;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform firepoint;
    public bool canShot { get; private set; } = true;
    void Start()
    {
        
    }

    public void Shot()
    {
        if (canShot)StartCoroutine(IEShot());
    }

    IEnumerator IEShot()
    {
        canShot = false;
        var p = Instantiate(projectile, firepoint.position, firepoint.rotation);
        p.GetComponent<Rigidbody2D>().AddForce(firepoint.up * projectileSpeed);
        yield return new WaitForSeconds(60f / fireRate);
        canShot = true;
    }
}
