using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : GenEnemy
{
    private FireProjectilesComponent shootFireballs;
    private float oldSpeed;

    public override void Start()
    {
        base.Start();
        oldSpeed = MovementControllerComponent.Speed;
        shootFireballs = GetComponentInChildren<FireProjectilesComponent>();
    }

    public override void UpdateBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // if player is > 10 away shoot fireballs
        if (distanceToPlayer >= 7 && distanceToPlayer <= 20) {
            MovementControllerComponent.SetSpeed(0);
            animationMachine.playAnimation("Stationary_Shoot", true, false, this);
            shootFireballs.willFire = true;
        }
        // if player is < 2 away attack
        else if (distanceToPlayer <= 3) {
            shootFireballs.willFire = false;
            MovementControllerComponent.SetSpeed(0);
            animationMachine.playAnimation("Attack_Melee", false, false, this);
        }
        else
        {
            shootFireballs.willFire = false;
            MovementControllerComponent.SetSpeed(3);

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