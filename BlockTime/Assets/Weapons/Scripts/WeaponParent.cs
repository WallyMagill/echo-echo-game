using System.Collections;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public Vector2 PointerPosition { get; set; }

    private Vector2 direction;
    private SpriteRenderer weaponRenderer;
    private HitboxComponent weaponHitbox;
    private Weapon weapon;

    public void Equip(GameObject weaponObj)
    {
        // If trying to equip a weapon
        if (weaponObj != null)
        {
            weapon = weaponObj.GetComponent<Weapon>();
            weaponRenderer = weaponObj.GetComponent<SpriteRenderer>();
            weaponHitbox = weaponObj.GetComponent<HitboxComponent>();
            if (weaponHitbox != null)
            {
                weaponHitbox.SetIsActive(false);
            }
        }
        // If equipping nothing
        else
        {
            weapon = null;
            weaponRenderer = null;
            weaponHitbox = null;
        }
    }

    private void Update()
    {
        // Rotate the weapon to face the pointer
        direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        
        if (weaponRenderer != null)
        {
            // Set and flip the weapon scale if needed
            if (direction.x < 0)
            {
                weaponRenderer.flipY = true;
            }
            else
            {
                weaponRenderer.flipY = false;
            }
            // Update weapon sorting order based on rotation angle
            float angle = transform.eulerAngles.z;
            if ((angle > 0 && angle < 90) || (angle < 360 && angle > 270))
            {   
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            }
            else
            {
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
            }
        }
    }

    public void Attack()
    {
        if (weapon) weapon.Attack();
    }

    public Vector2 getDirection()
    {
        return direction;
    }
}
