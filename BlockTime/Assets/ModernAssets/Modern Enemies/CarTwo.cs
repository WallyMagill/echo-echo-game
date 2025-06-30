// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarTwo : GenEnemy
// {
    
//     public Transform respawnPoint; // Assign this in the Inspector
//     public GameObject carPrefab;
    
//     public override void UpdateBehavior()
//     {
//         animationMachine.playAnimation("Walk", true, false, this);
//         stateMachine.TrySetState(patrolState, this);
//     }





//     private void OnDisable() // Called when object is destroyed
//     {
//         Respawn();
//     }
    
//     private void Respawn()
// {
//     if (respawnPoint != null)
//     {
//         GameObject newCar = Instantiate(carPrefab, respawnPoint.position, Quaternion.identity);
//         newCar.name = carPrefab.name; // Keep the same name

//         // Make sure to set the state machine or any necessary components for the new car
//         var newCarScript = newCar.GetComponent<CarTwo>();
//         if (newCarScript != null)
//         {
//             newCarScript.stateMachine.TrySetState(newCarScript.patrolState, newCarScript); // Initialize state machine
//         }
//     }
//     else
//     {
//         Debug.LogWarning("RespawnPoint is not set for " + carPrefab.name);
//     }
// }

// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTwo : GenEnemy
{
    // private void Start()
    // {
    //     // Call FlipAtStart when the car is created to set its initial direction
    //     FlipAtStart();
    // }

    public override void UpdateBehavior()
    {
        // Play the walking animation
        animationMachine.playAnimation("Walk", true, false, this);

        // Set the state of the car to patrol state
        stateMachine.TrySetState(patrolState, this);

        // Call method to flip the sprite based on the vertical movement direction
        FlipSpriteVertically();
    }

    // Method to flip the sprite vertically depending on movement direction
    private void FlipSpriteVertically()
    {
        if (movementControllerComponent != null)
        {
            // Get the movement direction
            Vector2 movementDirection = movementControllerComponent.Direction;

            // Check if the enemy is moving vertically
            if (movementDirection.y != 0)
            {
                // Flip the sprite or adjust animation direction based on the vertical movement
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Sign(movementDirection.y) * Mathf.Abs(transform.localScale.y), transform.localScale.z);
            }
        }
    }

    // If you just want to flip the sprite once at the start of the game (without using Start())
    private void FlipAtStart()
    {
        //transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z); // Flip the Y axis
    }
}




