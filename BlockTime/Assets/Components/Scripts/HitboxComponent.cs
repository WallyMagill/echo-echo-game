using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Enforces a 2D collider
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10; // Customize for each entity's damage
    [SerializeField] private bool isActive = true;

    private Collider2D colliderComponent;

    private void Awake()
    {
        colliderComponent = GetComponent<Collider2D>();

        // Ensure the collider is a trigger to allow attack without physical collision response
        if (colliderComponent != null)
        {
            colliderComponent.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("Hitbox requires a Collider2D component to function.");
        }

        // Sets layer as Hitbox so it can only collide with a Hurtbox
        int hitboxLayer = LayerMask.NameToLayer("Hitbox");
        if (hitboxLayer != -1) // Ensure the layer exists
        {
            gameObject.layer = hitboxLayer;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the other object has a hurtbox component
        HurtboxComponent hurtbox = other.GetComponent<HurtboxComponent>();
        if (isActive && hurtbox != null)
        {
            GameObject hitbox_parent = gameObject.transform.parent?.gameObject;
            GameObject hurtbox_parent = hurtbox.transform.parent?.gameObject;

            // Don't deal damage if hitbox's and hurtbox's parent have the same tag
            if (hitbox_parent.tag == hurtbox_parent.tag || hurtbox_parent.tag == "Player" && hitbox_parent.tag == "PlayerProjectile")
            {
                return;
            }

            hurtbox.TakeDamage(damageAmount); // Apply damage
        }
    }

    public void SetIsActive(bool status)
    {
        isActive = status;
    }
}
