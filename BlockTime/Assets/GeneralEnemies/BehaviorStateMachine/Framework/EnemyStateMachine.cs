using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;

    // Method to forcefully set the state
    private void SetState(IEnemyState newState, GenEnemy enemy)
    {
        currentState?.ExitState(enemy);
        currentState = newState;
        currentState.EnterState(enemy);
    }

    // Method to set the state only if it's different from the current state
    public void TrySetState(IEnemyState newState, GenEnemy enemy)
    {
        // Check if the new state is the same as the current state
        if (newState == null || currentState == newState)
        {
            return;  // If the same, do nothing
        }
        
        // If different, exit the current state and enter the new state
        SetState(newState, enemy);
    }

    public void UpdateState(GenEnemy enemy)
    {
        currentState?.UpdateState(enemy);
    }
}
