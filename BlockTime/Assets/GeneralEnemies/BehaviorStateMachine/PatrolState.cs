using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private float intitalSpeed;

    public void EnterState(GenEnemy enemy)
    {
        intitalSpeed = enemy.MovementControllerComponent.Speed;

        // if there is a path to patrol, patrol (edge case checking, don't delete)
        if (enemy.currentPatrolIndex < enemy.patrolPoints.Count) {
            SetDirectionToNextPatrolPoint(enemy);
        }
        // else, stand still
        else {
            enemy.MovementControllerComponent.SetSpeed(0);
        }
    }

    public void UpdateState(GenEnemy enemy)
    { 
        // Check if the enemy is close to the current patrol point (& that there is a path to patrol)
        if (enemy.currentPatrolIndex < enemy.patrolPoints.Count && Vector2.Distance(enemy.transform.position, enemy.patrolPoints[enemy.currentPatrolIndex].position) < 0.1f)
        {
            // Move to the next patrol point, looping back to the first point if at the end
            enemy.currentPatrolIndex++;
            if (enemy.currentPatrolIndex >= enemy.patrolPoints.Count)
            {
                enemy.currentPatrolIndex = 0;  // Reset to the first patrol point
            }

            // Set the direction to the new target patrol point
            SetDirectionToNextPatrolPoint(enemy);
        }
    }

    public void ExitState(GenEnemy enemy) 
    { 
        /* Cleanup patrol if needed */ 
        enemy.MovementControllerComponent.SetSpeed(intitalSpeed);
    }

    private void SetDirectionToNextPatrolPoint(GenEnemy enemy)
    {
        // Calculate direction to the next patrol point
        Vector2 targetPosition = enemy.patrolPoints[enemy.currentPatrolIndex].position;
        Vector2 directionToTarget = (targetPosition - (Vector2)enemy.transform.position).normalized;
        enemy.MovementControllerComponent.SetDirection(directionToTarget);
    }
}
