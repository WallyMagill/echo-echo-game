// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarEnemyMovement : GenEnemy
// {
//     public float speed = 5f; 
//     public bool movesDown = true;

//     private void Start()
//     {
//         Debug.Log("Started CarEnemy");

//         // Ensure movementControllerComponent is assigned
//         if (movementControllerComponent == null)
//         {
//             movementControllerComponent = GetComponent<MovementControllerComponent>();
//         }

//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(speed);
//             movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);

//             Debug.Log("Car direction set to: " + (movesDown ? "Down" : "Up"));
//         }
//         else
//         {
//             Debug.LogError("MovementControllerComponent is missing on: " + gameObject.name);
//         }
//     }



//     public override void UpdateBehavior()
//     {
        
//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(speed); 
            
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
// }


//March 1 
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarEnemyMovement : CarGenEnemy
// {
//     //public float speed = 5f; 
//     //public bool movesDown = true;

//     private void Start()
//     {
//         Debug.Log("Started CarEnemy");

//         // Ensure movementControllerComponent is assigned
//         if (movementControllerComponent == null)
//         {
//             movementControllerComponent = GetComponent<MovementControllerComponent>();
//         }

//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(speed);
//             movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);

//             Debug.Log("Car direction set to: " + (movesDown ? "Down" : "Up"));
//         }
//         else
//         {
//             Debug.LogError("MovementControllerComponent is missing on: " + gameObject.name);
//         }
//     }

//     public override void UpdateBehavior()
//     {
//         // Check if the car is firing projectiles
//         if (fireProjectilesComponent != null && fireProjectilesComponent.willFire)
//         {
//             // Play the attack animation when firing
//             animationMachine.playAnimation("Attack_Melee", false, false, this);
//         }
//         else
//         {
//             // Play the walk animation if not attacking
//             animationMachine.playAnimation("Walk", true, false, this);
//         }

//         // Handle movement logic
//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(speed);
            
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
// }



//march 5
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarEnemyMovement : GenEnemy
// {
//     public bool movesDown = true;
//     private float previousHealth;
//     private bool isStaggering = false;  // Flag to check if stagger is already playing

//     private HurtboxComponent carHurtboxComponent;

//     private void Start()
//     {
//         Debug.Log("Started CarEnemy");

//         // Find the HurtboxComponent on the child object named "Hurtbox"
//         carHurtboxComponent = transform.Find("Hurtbox")?.GetComponent<HurtboxComponent>();

//         if (carHurtboxComponent != null)
//         {
//             Debug.Log("HurtboxComponent found with initial health: " + carHurtboxComponent.Health);
//             previousHealth = carHurtboxComponent.Health;
//         }
//         else
//         {
//             Debug.LogError("HurtboxComponent not found on child object!");
//         }

//         // Ensure movementControllerComponent is assigned
//         if (movementControllerComponent == null)
//         {
//             movementControllerComponent = GetComponent<MovementControllerComponent>();
//         }

//         if (movementControllerComponent != null)
//         {
//             movementControllerComponent.SetSpeed(2f);  // Use the inherited speed from GenEnemy

//             // Set direction based on movesDown
//             movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);

//             Debug.Log("Car direction set to: " + (movesDown ? "Down" : "Up"));
//         }
//         else
//         {
//             Debug.LogError("MovementControllerComponent is missing on: " + gameObject.name);
//         }
//     }
//     // Ensure UpdateBehavior is being called every frame
//     private void Update()
//     {
//         UpdateBehavior();  // Explicitly call UpdateBehavior() here if it's not called from the base class
//     }
//     // Update is called once per frame
//     public override void UpdateBehavior()
//     {
//         // Check if the health has changed (indicating a hit)
//         if (carHurtboxComponent != null)
//         {
//             Debug.Log("Current Health: " + carHurtboxComponent.Health + ", Previous Health: " + previousHealth);

//             if (carHurtboxComponent.Health < previousHealth)
//             {
//                 Debug.Log("Car is being hurt! Current Health: " + carHurtboxComponent.Health + ", Previous Health: " + previousHealth);

//                 // If health has decreased and stagger animation isn't already playing, play it
//                 if (!isStaggering)
//                 {
//                     animationMachine.playAnimation("Stagger", false, true, this);
//                     isStaggering = true;  // Set flag to true to prevent retriggering
//                 }

//                 previousHealth = carHurtboxComponent.Health;  // Update the previous health value
//             }
//             else
//             {
//                 // If health hasn't changed, continue with the normal behavior (idle/walk)
//                 Debug.Log("Car is not hurt. Continuing with regular behavior.");

//                 // Only play normal animations if we're not staggering
//                 if (!isStaggering)
//                 {
//                     // Check if the car is firing projectiles
//                     if (fireProjectilesComponent != null && fireProjectilesComponent.willFire)
//                     {
//                         animationMachine.playAnimation("Attack_Melee", false, false, this);
//                     }
//                     else
//                     {
//                         // Play the walk animation if not attacking
//                         animationMachine.playAnimation("Walk", true, false, this);
//                     }
//                 }
//             }
//         }
//         else
//         {
//             Debug.LogError("carHurtboxComponent is null. Cannot check health.");
//         }

//         // If health is less than or equal to zero, destroy the car
//         if (carHurtboxComponent != null && carHurtboxComponent.Health <= 0)
//         {
//             Debug.Log("Car health is 0 or below, playing death animation.");
//             animationMachine.playAnimation("Death", false, true, this);
//             Destroy(gameObject);  // Destroy the car GameObject
//         }
//     }

//     // This method can be called if the car is hit, to simulate stagger
//     public void OnCarHit()
//     {
//         Debug.Log("OnCarHit triggered, playing stagger animation.");
//         if (!isStaggering)
//         {
//             animationMachine.playAnimation("Stagger", false, true, this);
//             isStaggering = true;
//         }
//     }

//     // Reset stagger status when animation finishes (or after a specific time, e.g., 1 second)
//     public void ResetStagger()
//     {
//         isStaggering = false;  // Reset stagger state once the stagger animation has finished
//         Debug.Log("Stagger animation finished, resetting state.");
//     }
// }





//March 6
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarEnemyMovement : GenEnemy
// {
//     public bool movesDown = true;
//     private float previousHealth;
//     private bool isStaggering = false;  // Flag to check if stagger is already playing

//     private HurtboxComponent carHurtboxComponent;

//     // Update is called once per frame
//     public override void UpdateBehavior()
//     {
//         movementControllerComponent.SetSpeed(2f);
//         movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);

//         // Play the walking animation
//         animationMachine.playAnimation("Walk", true, false, this);
//         stateMachine.TrySetState(patrolState, this);
//     }


// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemyMovement : GenEnemy
{
    public bool movesDown = true;
    
    // Update is called once per frame
    public override void UpdateBehavior()
    {
        movementControllerComponent.SetSpeed(2f);
        movementControllerComponent.SetDirection(movesDown ? Vector2.down : Vector2.up);

        // Flip the sprite when moving up
        FlipSprite();

        // Play the walking animation
        animationMachine.playAnimation("Walk", true, false, this);
        stateMachine.TrySetState(patrolState, this);
    }

    // Method to flip the sprite when moving up
    private void FlipSprite()
    {
        if (!movesDown) // If the car is moving up
        {
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else // If the car is moving down
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
    }

}
