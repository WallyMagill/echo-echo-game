using UnityEngine;

public class Shotgun : RangedWeapon
{
    public float spread = 20f;          // Degrees of spread of the shotgun
    public int numProjectiles = 5;     // Number of projectiles thrown

    protected override void FireProjectile()
    {
        // Check nulls then create projectiles in a spread
        if (projectilePrefab != null && firePoint != null)
        {
            // Make numProjectiles new projectiles
            for (int n = 0; n < numProjectiles; n++)
            {
                // Get random angle offset in the spread range and add to firePoint orientation
                float offset = Random.Range(-spread/2f, spread/2f);
                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, offset);

                // Create new projectile with proper orientation
                GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, rotation);
                Projectile projComponent = newProjectile.GetComponent<Projectile>();

                // Set the damage of the projectile
                if (projComponent != null)
                {
                    projComponent.SetDamage(damage);
                }
            }

            // Play the recoil animation
            animator.SetTrigger("Attack");
        }
    }
}