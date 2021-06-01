using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private float radius;
    public float speed;
    public GameObject Hiteffect;

    protected virtual void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        Destroy(gameObject, 10f);
    }
    protected virtual void OnHit(Character character)
    {
        if (character) character.TakeDamage(damage);
        GameObject effect = Instantiate(Hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temp = collision.collider.GetComponent<Character>();
        OnHit(temp);
    }

}
