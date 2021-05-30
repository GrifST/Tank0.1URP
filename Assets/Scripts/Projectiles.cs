using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    public GameObject Hiteffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temp = collision.collider.GetComponent<Character>();
        if (temp!=null)
        {
         temp.TakeDamage(damage);   
        }

        GameObject effect = Instantiate(Hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

}
