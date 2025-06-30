using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : GenEnemy
{
    private HitboxComponent axeHitbox;

    public override void Start()
    {
        base.Start();
        HitboxComponent[] hitboxes = GetComponentsInChildren<HitboxComponent>();
        
        // Find the specific hitbox by GameObject name
        foreach (HitboxComponent hitbox in hitboxes)
        {
            if (hitbox.gameObject.name == "AxeHitbox") // Replace with the actual name
            {
                axeHitbox = hitbox;
                break; // Stop searching after finding the right one
            }
        }
    }

    public override void UpdateBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // if player is within chase distance, chase, and if within attack distance, attack
        // else, wander about

        if (distanceToPlayer <= 2) {
            axeHitbox.SetIsActive(true);
            animationMachine.playAnimation("Attack_Melee", false, false, this);
        }
        else
        {
            axeHitbox.SetIsActive(false);
            // if standing, idle, if moving, walk
            if (MovementControllerComponent.Speed == 0) {
                animationMachine.playAnimation("Idle", true, false, this);
            }
            else {
                animationMachine.playAnimation("Walk", true, false, this);
            }

            if (distanceToPlayer <= chaseNoticeDist) {
                stateMachine.TrySetState(basicChaseState, this);
            }
            else {
                stateMachine.TrySetState(wanderState, this);
            }
        }
    }
}