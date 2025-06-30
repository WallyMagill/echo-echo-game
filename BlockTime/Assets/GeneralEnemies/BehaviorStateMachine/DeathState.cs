using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IEnemyState
{
    public void EnterState(GenEnemy enemy)
    {
        // Stop movement and trigger any death-related visuals/animations
        if (enemy.MovementControllerComponent != null) {    
            enemy.MovementControllerComponent.SetSpeed(0);
        }        

        //Debug.Log($"{enemy.gameObject.name} is in the death state.");

        // Start the wait-for-death-animation coroutine
        if (enemy.Animator != null) {
            enemy.StartDeathCoroutine();
        }
    }

    public void UpdateState(GenEnemy enemy)
    {

    }

    public void ExitState(GenEnemy enemy)
    {
        // Destroy the game object upon exiting the death state
        GameObject.Destroy(enemy.gameObject);
    }
}