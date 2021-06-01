using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectiles : Projectiles
{
    private float timer;
    protected override void Start()
    {
        transform.localScale = new Vector3(0, 1, 1);
        Destroy(gameObject, 1f);
    }
    protected override void OnHit(Character character)
    {
        if (character) character.TakeDamage(damage);
        GameObject effect = Instantiate(Hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
    private void FixedUpdate()
    {
        if(timer > 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.05f,1,1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.05f, 1, 1);
        }
        timer += Time.deltaTime;
    }
}
