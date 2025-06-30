using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CameraShake cameraShake;
    public AudioSource footstepSoundSource;
    public AudioSource hurtSoundSource;
    public AudioSource dashSoundSource;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeedMultiplier = 8f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] public Slider dashSlider;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator _animator;
    private HurtboxComponent hurtbox;

    // For dashing
    private Image dashFillImage;
    private Color originalDashBarColor;
    private bool isDashing = false;
    private float dashTime = 0f;
    private float dashCooldownTime = 0f;

    // for audio
    [SerializeField] private float footstepInterval = 9f; // Adjust for pacing
    private float footstepTimer = 0f;
    [SerializeField] private float dashSoundInterval = 0.75f; // Adjust for pacing
    private float dashSoundTimer = 0f;

    // for health
    private float oldHealth;

    // For weapons
    private Vector2 pointerPosition;
    private WeaponParent weaponParent;
    [SerializeField] private InputActionReference primaryAttack;

    // -----------------------------------------------------------------------------------
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        hurtbox = GetComponentInChildren<HurtboxComponent>();
        weaponParent = GetComponentInChildren<WeaponParent>();

        // for dash bar
        dashFillImage = dashSlider.GetComponentInChildren<Image>();
        originalDashBarColor = dashFillImage.color;

        // remember health to check if hurt
        if (hurtbox != null)
        {
            oldHealth = hurtbox.Health;
        }
        else {
            Debug.Log("Player hurtbox is null");
        }
    }

    // -----------------------------------------------------------------------------------
    void Update()
    {
        // Get pointer position and pick correct animation
        pointerPosition = getPointer();
        UpdateMouseParameters(pointerPosition);
        weaponParent.PointerPosition = pointerPosition;

        // Get WASD movement input
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        // Check if the player is moving
        bool isMoving = movement != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);

        // Update direction-specific movement booleans
        UpdateDirectionalAnimatorBooleans();

        // Dash
        PlayerDash(isMoving);

        // apply footstep audio accordingly
        if (isMoving && !isDashing)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f) 
            {
                footstepSoundSource.PlayOneShot(footstepSoundSource.clip);
                footstepTimer = footstepInterval; // Reset timer
            }
        }
        else if (isDashing)
        {
            dashSoundTimer -= Time.deltaTime;

            if (dashSoundTimer <= 0f) 
            {
                dashSoundSource.PlayOneShot(dashSoundSource.clip);
                dashSoundTimer = dashSoundInterval; // Reset timer
            }
        }
        else
        {
            footstepSoundSource.Stop();
            footstepTimer = 0f; // Reset when stopping
            dashSoundTimer = 0f;
        }

        // Apply movement with adjusted speed
        float currentSpeed = isDashing ? moveSpeed * dashSpeedMultiplier : moveSpeed;
        rb.velocity = movement.normalized * currentSpeed;
        // // Detect walls ahead of movement
        // Vector2 nextPosition = rb.position + movement.normalized * 0.1f;
        // Collider2D hitWall = Physics2D.OverlapCircle(nextPosition, 0.2f, LayerMask.GetMask("Walls"));

        // if (hitWall == null) 
        // {
        //     rb.velocity = movement.normalized * currentSpeed; // Move if no wall is hit
        // }
        // else
        // {
        //     rb.velocity = Vector2.zero; // Stop movement if a wall is hit
        // }



        // Update Animator dash state
        _animator.SetBool("IsDashing", isDashing);

        // Check for damage taken
        if (hurtbox.Health < oldHealth) {
            oldHealth = hurtbox.Health;

            _animator.SetTrigger("Hit"); // Trigger the animation
            StartCoroutine(cameraShake.Shake(0.1f, 0.02f)); 
            hurtSoundSource.PlayOneShot(hurtSoundSource.clip);
            StartCoroutine(ResetHitTrigger()); // Reset after a delay
        }
    }

    // -----------------------------------------------------------------------------------
    private void PlayerDash(bool isMoving)
    {
        // Check if dash can be started
        if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving && !isDashing && dashCooldownTime <= 0f)
        {
            isDashing = true;
            dashTime = dashDuration;
        }

        // Update dash timer
        if (isDashing)
        {            
            dashTime -= Time.deltaTime;

            // Decrease slider as the dash progresses
            dashSlider.value = Mathf.Lerp(0, 10, 1 - (dashTime * dashDuration)); // 0 to 10 scale

            if (dashTime <= 0)
            {
                isDashing = false;
                dashCooldownTime = dashCooldown; // Start cooldown
            }
        }
        // Update dash cooldown timer
        else if (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime;
            // Increase slider back to 10 during cooldown
            dashSlider.value = Mathf.Lerp(0, 10, 1 - (dashCooldownTime * dashCooldown)); // 0 to 10 scale
        }

        // Update the fill color based on slider value
        UpdateDashBarColor();
    }

    private void UpdateDashBarColor()
    {
        Color dimmedColor = originalDashBarColor * 0.85f; // Reduce brightness

        // Compare slider value with max value
        if (dashSlider.value < dashSlider.maxValue)
        {
            dashFillImage.color = dimmedColor; // Darker tint when not full
        }
        else
        {
            dashFillImage.color = originalDashBarColor; // Normal color when full
        }
    }

    // -----------------------------------------------------------------------------------
    // On click tell the weapon parent to perform an attack
    private void OnClick(InputAction.CallbackContext context)
    {
        weaponParent.Attack();
    }

    private void OnEnable()
    {
        primaryAttack.action.performed += OnClick;
    }

    private void OnDisable()
    {
        primaryAttack.action.performed -= OnClick;
    }

    // -----------------------------------------------------------------------------------
    private Vector2 getPointer()
    {
        Vector3 mousePosition = Input.mousePosition;
        return (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void UpdateMouseParameters(Vector2 mousePosition)
    {
        // Calculate the direction and angle to the mouse
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update the Animator parameters for the blend tree
        _animator.SetFloat("MouseX", direction.x);
        _animator.SetFloat("MouseY", direction.y);
    }

    private IEnumerator ResetHitTrigger()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure transition starts
        _animator.ResetTrigger("Hit");
    }

    private void UpdateDirectionalAnimatorBooleans()
    {
        // Set cardinal movement booleans
        _animator.SetBool("MoveE", movement.x > 0 && movement.y == 0); // Moving east
        _animator.SetBool("MoveW", movement.x < 0 && movement.y == 0); // Moving west
        _animator.SetBool("MoveN", movement.y > 0 && movement.x == 0); // Moving north
        _animator.SetBool("MoveS", movement.y < 0 && movement.x == 0); // Moving south

        // Set diagonal movement booleans
        _animator.SetBool("MoveNE", movement.x > 0 && movement.y > 0); // Moving northeast
        _animator.SetBool("MoveNW", movement.x < 0 && movement.y > 0); // Moving northwest
        _animator.SetBool("MoveSE", movement.x > 0 && movement.y < 0); // Moving southeast
        _animator.SetBool("MoveSW", movement.x < 0 && movement.y < 0); // Moving southwest
    }

}
