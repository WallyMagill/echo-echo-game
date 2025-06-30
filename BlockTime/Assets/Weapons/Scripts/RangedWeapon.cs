using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedWeapon : Weapon
{    
    public float attackDelay = 0.2f;
    public float reloadDelay = 1f;
    public int maxAmmo = 1000000;
    public int damage = 20;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private bool canShoot = true;
    private bool isReloading = false;

    private int ammo;

    protected override void Start()
    {
        base.Start();
        ammo = maxAmmo;
    }

    public override void Attack()
    {
        // Immediately return if waiting for shooting delay or reloading
        if (!canShoot || isReloading || ammo <= 0) return;

        StartCoroutine(ShootRoutine());
    }

    protected virtual void FireProjectile()
    {
        // Check nulls then create projectile and set the damage
        if (projectilePrefab != null && firePoint != null)
        {
            // Shoot the projectile
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projComponent = newProjectile.GetComponent<Projectile>();

            // Play the recoil attack animation
            animator.SetTrigger("Attack");

            if (projComponent != null)
            {
                projComponent.SetDamage(damage);
            }
        }
    }

    private IEnumerator ShootRoutine()
    {
        // Wait for attack delay before being able to send next projectile
        canShoot = false;
        ammo--;
        FireProjectile();

        yield return new WaitForSecondsRealtime(attackDelay);

        canShoot = true;

        if (ammo <= 0)
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    private IEnumerator ReloadRoutine()
    {
        // Wait for reload delay
        isReloading = true;

        yield return new WaitForSecondsRealtime(reloadDelay);

        ammo = maxAmmo;
        isReloading = false;
        canShoot = true;
    }
}
