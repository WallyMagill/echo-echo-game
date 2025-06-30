using System.Collections;
using UnityEngine;

public class CarProjectileDestroy : MonoBehaviour
{
    public float destroyDelay = 0.1f; // Optional delay before destroying the bullet

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug to see what the bullet hit
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        // Destroy the bullet after the delay
        Destroy(gameObject, destroyDelay);
    }
}
