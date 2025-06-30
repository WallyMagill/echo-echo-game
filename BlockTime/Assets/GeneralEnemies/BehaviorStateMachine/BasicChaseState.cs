using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChaseState : IEnemyState
{
    // TODO: have different versions of the chase state: basic chase, chase when in line of sight, sudden dash chase

    public void EnterState(GenEnemy enemy)
    { 
        /* Start chasing player animation */ 
    }

    public void UpdateState(GenEnemy enemy)
    { 
        // Set direction towards the player each frame to continuously chase
        // TODO: Need to set up pathfinding so avoids enemies
        Vector2 directionToPlayer = (enemy.player.position - enemy.transform.position).normalized;

        if (enemy.MovementControllerComponent != null) {
            enemy.MovementControllerComponent.SetDirection(directionToPlayer);
        }
    }

    public void ExitState(GenEnemy enemy)
    {
        /* Cleanup chase if needed */
    }
}
