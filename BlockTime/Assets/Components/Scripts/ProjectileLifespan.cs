using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifespan : MonoBehaviour
{
    public float lifetime = 5f; // Set the lifetime in seconds in the Inspector

    private void Start()
    {
        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

}
