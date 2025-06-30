using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iza : GenEnemy
{
    public override void UpdateBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // if player is within chase distance, chase
        // else, wander about

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
