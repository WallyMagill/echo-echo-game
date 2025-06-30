using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private bool attackBlocked = false;
    private HitboxComponent weaponHitbox;

    public float attackDelay = 0.2f;
    
    protected override void Awake()
    {
        base.Awake();
        weaponHitbox = GetComponentInChildren<HitboxComponent>();
        if (weaponHitbox != null) {
            weaponHitbox.SetIsActive(false);
        }
    }

    public override void Attack()
    {
        if (attackBlocked)
        {
            return;
        }
        if (weaponHitbox != null) {
            weaponHitbox.SetIsActive(true);
        }
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        if (weaponHitbox != null) {
            weaponHitbox.SetIsActive(false);
        }
        attackBlocked = false;
    }
}
