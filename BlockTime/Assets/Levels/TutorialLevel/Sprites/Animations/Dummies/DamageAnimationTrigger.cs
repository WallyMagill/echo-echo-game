using UnityEngine;

public class HealthAnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private HurtboxComponent hurtboxComponent;

    private float lastHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hurtboxComponent = GetComponentInChildren<HurtboxComponent>();

        if (hurtboxComponent != null)
        {
            lastHealth = hurtboxComponent.Health;
        }
        else
        {
            Debug.LogWarning("HurtboxComponent not found. Ensure it's attached to a child of this object.");
        }
    }

    private void Update()
    {
        if (hurtboxComponent != null && animator != null)
        {
            // Check if the health has decreased
            if (hurtboxComponent.Health < lastHealth)
            {
                // Check if the hurt animation is currently playing
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dummy 1"))
                {
                    // Trigger the hurt animation if it isn't already playing
                    animator.SetTrigger("PlayHurt");
                }

                // Update lastHealth to the current health
                lastHealth = hurtboxComponent.Health;
            }
        }
    }
}
