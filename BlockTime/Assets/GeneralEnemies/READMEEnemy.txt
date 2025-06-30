How to Create an Enemy:

Animation---------------:
1) Add an Animator

2) To the Animator, attach an Animation Override Controller that takes the EnemyAnimationControllerComponent and input the Animation Clips you want for the thing

3) Ensure that the Animation Clips that need to be looped are looped eg. idle, walk, etc


Enemy AI---------------: 
1) Attach "MovementControllerComponent" script and change movement speed as you see fit

2) If you want it to shoot projectiles add the "FireProjectilesComponent" script

3) Create and attach a new enemy script that inherits from the GenEnemy class, and override the UpdateMovementStates() method with the movement pattern you want. You can consult the GenEnemy script to see what an example looks like.

4) Modify any needed variables on the child script inspector as needed


Hitboxes and Hurtboxes---------------:
1) To add a hitbox, add the Hitbox prefab

2) Adjust the damage dealt in the inspector as you see fit

3) To add a hurtbox, add the Hurtbox prefab

4) Adjust the starting health amount in the inspector as you see fit

Rigid Body?? --------------:
- Ask Wally

1) If you want it to interact with the world, add a Rigidbody 2D, type dynamic, simulated, continuous collision detection
^^ IT NEEDS TO BE THE LAST COMPONENT (add at feet)

2) Configure the Mass, Linear Drag, and Angular Drag as appropriate