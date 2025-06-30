using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 0f; // Projectile lifetime, 0 for infinite lifetime
    private int damageAmount;
    public float projectileSpeed = 4f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = gameObject.transform.right.normalized * projectileSpeed;

        if (lifetime != 0)
        {
            Destroy(gameObject, lifetime);
        }
    }

    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtboxComponent hurtboxComponent = collision.gameObject.GetComponent<HurtboxComponent>();

        // if hit the enemy's rigid body, ignore
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (collision.GetComponent<Rigidbody2D>() != null)
            {
                return;
            }
        }

        if (collision.gameObject.tag.Equals("Walls") || collision.gameObject.tag.Equals("Enemy"))
        {
            if (hurtboxComponent)
            {
                hurtboxComponent.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
