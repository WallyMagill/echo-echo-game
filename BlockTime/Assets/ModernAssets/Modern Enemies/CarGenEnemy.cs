// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarGenEnemy : GenEnemy
// {
//     public bool movesDown = true;

//     private void Start()
//     {
//         Debug.Log("Started CarGenEnemy");

//         if (movementControllerComponent == null)
//         {
//             movementControllerComponent = GetComponent<MovementControllerComponent>();
//         }

//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(5f); // Setting speed properly
//             movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);
//             Debug.Log("Car direction set to: " + (movesDown ? "Down" : "Up"));
//         }
//         else
//         {
//             Debug.LogError("MovementControllerComponent is missing on: " + gameObject.name);
//         }

//         // Subscribe to hurtbox hit event if available
//         if (hurtboxComponent != null)
//         {
//             hurtboxComponent.OnHit += HandleHurtboxHit;
//         }
//     }

//     public override void UpdateBehavior()
//     {
//         if (fireProjectilesComponent != null && fireProjectilesComponent.willFire)
//         {
//             animationMachine.playAnimation("Attack_Melee", false, false, this);
//         }
//         else
//         {
//             animationMachine.playAnimation("Walk", true, false, this);
//         }

//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(5f); // Ensure speed is maintained

//             if (movesDown)
//             {
//                 movementControllerComponent.SetDirection(Vector2.down);
//             }
//             else
//             {
//                 movementControllerComponent.SetDirection(Vector2.up);
//             }
//         }
//     }

//     // Handle getting hit (stagger effect)
//     private void HandleHurtboxHit(GameObject hitSource)
//     {
//         // Ignore damage from projectiles fired by the car itself
//         if (hitSource.CompareTag("CarBullet"))
//         {
//             return;
//         }

//         Debug.Log("Car hit! Playing stagger animation.");
//         animationMachine.playAnimation("Stagger", false, true, this);
//     }

//     private void OnDestroy()
//     {
//         if (hurtboxComponent != null)
//         {
//             hurtboxComponent.OnHit -= HandleHurtboxHit; // Unsubscribe from event to prevent memory leaks
//         }
//     }
// }
