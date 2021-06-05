using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Projectiles projectile;
    public float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform firepoint;

    private Animator animator;
    private Character character;
    public bool canShot { get; private set; } = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponentInParent<Character>();
    }

    public void Shot()
    {
        if (canShot)StartCoroutine(IEShot());
    }

    IEnumerator IEShot()
    {
        canShot = false;
        animator.Play("Shot");
        var p = Instantiate(projectile, firepoint.position, firepoint.rotation);
        p.owner = character;
        p.speed = projectileSpeed;
        yield return new WaitForSeconds(60f / fireRate);
        canShot = true;
    }
}
