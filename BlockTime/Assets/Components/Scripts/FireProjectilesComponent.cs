using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class FireProjectilesComponent : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject fireStart; // Position from where the projectile will be spawned
    public bool targetIsMouse = false;
    [HideIf("targetIsMouse")] public Transform target;

    [Header("Projectile Settings")]
    public bool willFire = false;
    public float projectileSpeed = 10f;
    public float fireRate = 1f;

    private float fireCooldown = 0f;

    // -----------------------------------------------------------------------------------
    private void Update()
    {
        // Update the target if it's set to the mouse
        if (targetIsMouse)
        {
            // Get mouse position in world space
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = transform.position.z; // Keep z consistent for 2D or top-down views

            if (target == null)
            {
                // Create a temporary GameObject to hold the mouse target position
                GameObject mouseTarget = new GameObject("MouseTarget");
                target = mouseTarget.transform;
            }
            target.position = mouseWorldPosition;
        }

        // Check if fire cooldown has elapsed
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Fire projectile if cooldown is over and input or trigger is active
        if (fireCooldown <= 0 && willFire && target != null)
        {
            FireProjectile();
            fireCooldown = 1f / fireRate; // Reset cooldown
        }
    }

    // -----------------------------------------------------------------------------------
    private void FireProjectile()
    {
        if (target == null)
        {
            Debug.LogWarning("No target Transform set for FireProjectiles");
            return;
        }

        // Get the position of the target
        Vector2 targetPoint = target.position;

        // Instantiate the projectile prefab at the fire point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, fireStart.transform.position, Quaternion.identity);

        // Calculate direction toward the target point
        Vector2 direction = (targetPoint - (Vector2)fireStart.transform.position).normalized;

        // Set the projectile's velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed; // Adjust direction as needed
        }

        // Set the projectile's rotation to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //numToShoot -= 1;
    }
}
