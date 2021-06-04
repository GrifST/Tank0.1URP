using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectiles projectile;
    public float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform firepoint;

    //[SerializeField] private Animator gunShot; анимация отката орудия
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
        //gunShot.Play("Base Layer");
        var p = Instantiate(projectile, firepoint.position, firepoint.rotation);
        p.speed = projectileSpeed;
        yield return new WaitForSeconds(60f / fireRate);
        canShot = true;
    }
}
