using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtboxComponent : MonoBehaviour
{   
    [SerializeField] private float health = 100;
    [SerializeField] private float invulnerabilityDuration = 0.25f;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;

    private Collider2D colliderComponent;

    private void Awake()
    {
        colliderComponent = GetComponent<Collider2D>();

        if (colliderComponent != null)
        {
            colliderComponent.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("Hurtbox requires a Collider2D component to function.");
        }

        // Sets layer as Hurtbox so it can only receive hits from a Hitbox
        int hurtboxLayer = LayerMask.NameToLayer("Hurtbox");
        if (hurtboxLayer != -1) // Ensure the layer exists
        {
            gameObject.layer = hurtboxLayer;
        }

    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            StartInvulnerability();
        }
    }
    // HAHA IM INVULNERABLE HAHAA LETS GOO MAN LETSS SGOOO HHAAPPY THANKsIGIVNG
    private void StartInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;
    }

    public float Health
    {
        get { return health; }
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
    }
}
