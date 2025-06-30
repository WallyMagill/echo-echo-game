using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IEnemyState
{
    private Vector2 wanderTarget;
    private float timer;
    private Vector2 initialPosition;
    private float intitalSpeed;

    public void EnterState(GenEnemy enemy)
    {
        initialPosition = enemy.transform.position; // Store the initial position when entering the state
        intitalSpeed = enemy.MovementControllerComponent.Speed;

        SetNewWanderTarget(enemy); // Set the initial target within the wander area
        timer = enemy.changeTargetInterval;
    }

    public void UpdateState(GenEnemy enemy)
    {
        // Move towards the current wander target
        Vector2 directionToTarget = (wanderTarget - (Vector2)enemy.transform.position).normalized;
        enemy.MovementControllerComponent.SetDirection(directionToTarget);

        if (Vector2.Distance(enemy.transform.position, wanderTarget) < 0.5f) // if have reached wander target
        {
            enemy.MovementControllerComponent.SetSpeed(0); // stop
        }
        else {
            enemy.MovementControllerComponent.SetSpeed(intitalSpeed);
        }

        timer -= Time.deltaTime;
        if (timer <= 0) // if timer has run out
        {
            SetNewWanderTarget(enemy);  // Pick a new random wander target
            timer = enemy.changeTargetInterval; // Reset timer
        }
    }

    public void ExitState(GenEnemy enemy)
    {
        enemy.MovementControllerComponent.SetSpeed(intitalSpeed);
    }

    private void SetNewWanderTarget(GenEnemy enemy)
    {
        // Calculate a new random target within the wander range from the initial position
        float randomX = Random.Range(-enemy.wanderRangeX, enemy.wanderRangeX);
        float randomY = Random.Range(-enemy.wanderRangeY, enemy.wanderRangeY);
        wanderTarget = initialPosition  + new Vector2(randomX, randomY);
    }
}
