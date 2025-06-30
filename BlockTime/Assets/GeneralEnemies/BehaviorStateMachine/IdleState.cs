using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private float intitalSpeed;

    public void EnterState(GenEnemy enemy)
    {
        // Stop the enemy movement
        if (enemy.MovementControllerComponent != null) {
            intitalSpeed = enemy.MovementControllerComponent.Speed;
            enemy.MovementControllerComponent.SetSpeed(0);
        }
    }

    public void UpdateState(GenEnemy enemy)
    {
        // Stay idle – no movement updates needed
    }

    public void ExitState(GenEnemy enemy)
    {
        // Nothing specific to reset or clean up for idle state
        if (enemy.MovementControllerComponent != null) {
            enemy.MovementControllerComponent.SetSpeed(intitalSpeed);
        }
    }
}

