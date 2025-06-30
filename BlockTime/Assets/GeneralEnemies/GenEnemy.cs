using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GenEnemy : MonoBehaviour
{
    // always needed components
    protected EnemyStateMachine stateMachine; 
    protected EnemyAnimationStateMachine animationMachine;
    protected Animator animator;

    // often needed components
    protected HurtboxComponent hurtboxComponent;
    protected MovementControllerComponent movementControllerComponent;
    protected FireProjectilesComponent fireProjectilesComponent;

    // player reference
    public Transform player;

    // intializing all potential states to go into
    protected IEnemyState idleState;
    protected IEnemyState patrolState;
    protected IEnemyState basicChaseState;
    protected IEnemyState wanderState;
    protected IEnemyState deathState;

    // -----------------------------------------------------------------------------------
    // health variables
    private float oldHealth;
    private bool dead = false;
    public event Action OnDeath;

    // respawn variables
    public bool willRespawn;
    protected Transform respawnLocation;

    // chase variables
    [Header("Chase Settings")]
    public bool willChase = false;
    [ShowIf("willChase")] public float chaseNoticeDist = 9999999999999;

    // patrol variables
    [Header("Patrol Settings")]
    [HideIf("willWander")] public bool willPatrol = false;
    [ShowIf("willPatrol")] public List<Transform> patrolPoints;  // List of patrol points
    [HideInInspector] public int currentPatrolIndex = 0;  // Start at the first patrol point

    // wander variables
    [Header("Wander Settings")]
    [HideIf("willPatrol")] public bool willWander = false;
    [ShowIf("willWander")] public float wanderRangeX = 2f; // dist from the initial pos that the enemy can wander
    [ShowIf("willWander")] public float wanderRangeY = 2f;
    [ShowIf("willWander")] public float changeTargetInterval = 2f;  // Time interval to pick a new wander target

    // -----------------------------------------------------------------------------------
    public virtual void Start()
    {
        // always needed components
        stateMachine = gameObject.AddComponent<EnemyStateMachine>();
        animationMachine = gameObject.AddComponent<EnemyAnimationStateMachine>();

        // often needed components
        animator = GetComponent<Animator>();
        hurtboxComponent = GetComponentInChildren<HurtboxComponent>();
        movementControllerComponent = GetComponent<MovementControllerComponent>();
        fireProjectilesComponent = GetComponent<FireProjectilesComponent>();

        // initialize states
        idleState = new IdleState();
        deathState = new DeathState();
        patrolState = new PatrolState();
        basicChaseState = new BasicChaseState();
        wanderState = new WanderState();

        // remember health to check if hurt
        if (hurtboxComponent != null)
        {
            oldHealth = hurtboxComponent.Health;
        }

        // Always default initialize to idle state
        stateMachine.TrySetState(idleState, this);
        animationMachine.playAnimation("Idle", true, false, this);
    }

    // -----------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        // TODO: LOOP THE ANIMATIONS
        // Update the current state
        stateMachine.UpdateState(this);

        bool health_change = UpdateStaggerAndDeathStates();

        if (!health_change)
        {
            UpdateBehavior();
        }

        // if movement controller, flip x axis as appropriate
        if (movementControllerComponent != null)
        {
            UpdateAnimationDirections();
        }
    }

    // -----------------------------------------------------------------------------------
    // ALERT: when inheritting from this genEnemy class, this is the method that you want to be overwriting 99% of the time
    // -----------------------------------------------------------------------------------
    public virtual void UpdateBehavior()
    {
        // NOTE: if we run into lag issues this is somewhere we can fix that by having some sort of signal that 
        // sets the state instead of trying to set the state you're in every frame
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //if within a certain distance chase, otherwise patrol
        if (distanceToPlayer <= 1) {
            // TODO: figure out what to do when two animations that don't get overwritten are both triggered (add willOverwrite param?)
            animationMachine.playAnimation("Attack_Melee", false, false, this);
            
            // stop firing projectiles
            if (fireProjectilesComponent != null) {
                fireProjectilesComponent.willFire = false;
            }
        }
        else
        {
            animationMachine.playAnimation("Walk", true, false, this);
            if (distanceToPlayer <= chaseNoticeDist) {
                // stop firing projectiles
                if (fireProjectilesComponent != null) {
                    fireProjectilesComponent.willFire = false;
                }

                stateMachine.TrySetState(basicChaseState, this);
            }
            else {
                // fire projectiles
                if (fireProjectilesComponent != null) {
                    fireProjectilesComponent.willFire = true;
                }
                stateMachine.TrySetState(patrolState, this);
            }
        }
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetChaseNoticeDistance(float distance)
    {
        chaseNoticeDist = distance;
    }

    // -----------------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------
    private bool UpdateStaggerAndDeathStates()
    {
        // TODO: need to figure out how to have the pause not just been for the animation but also for the stagger move pause/death wait
        
        bool health_change = false;

        // if hurtbox, make health checks
        if (hurtboxComponent != null)
        {
            // Check if health is zero or below, switch to DeathState if true
            if (hurtboxComponent.Health <= 0)
            {
                animationMachine.playAnimation("Death", false, true, this);
                // say that it's dead only once while the animation is playing out
                if (!dead) {
                    OnDeath?.Invoke();
                    dead = true;
                }
                stateMachine.TrySetState(deathState, this);
                health_change = true;
            }
            // if hurt, play stagger
            else if (hurtboxComponent.Health < oldHealth)
            {
                oldHealth = hurtboxComponent.Health;
                //stateMachine.TrySetState(idleState, this);
                animationMachine.playAnimation("Stagger", false, true, this);
                health_change = true;
            }
        }

        return health_change;
    }

    // -----------------------------------------------------------------------------------
    private void UpdateAnimationDirections()
    {
        if (animator == null) {return;}

        Vector2 movementDirection = movementControllerComponent.Direction;

        // Check if the enemy is moving horizontally
        if (movementDirection.x != 0)
        {
            // Flip the sprite or adjust animation direction based on the movement direction
            transform.localScale = new Vector3(Mathf.Sign(movementDirection.x)*Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }    
    }

    // -----------------------------------------------------------------------------------
    public void StartDeathCoroutine()
    {
        StartCoroutine(WaitForDeathAnimationToEnd());
    }

    // -----------------------------------------------------------------------------------
    private IEnumerator WaitForDeathAnimationToEnd()
    {
        // First, wait until the "Death" animation actually begins playing.
        while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null;  // Wait until the next frame
        }
       
        // Wait until the animation completes
        while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;  // Wait until the next frame
        }

        // move into the exit because we aren't going to set a new state
        deathState.ExitState(this);
    }

    // -----------------------------------------------------------------------------------
    public MovementControllerComponent MovementControllerComponent => movementControllerComponent;  // Public accessor
    public Animator Animator => animator;  // Public accessor
}
