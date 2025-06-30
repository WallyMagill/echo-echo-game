using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Enforces a 2D collider
public class ProximityText : MonoBehaviour
{
    public GameObject textObject; // The text object to show/hide
    public float disappearDelay = 7f; // Time before the text disappears

    private bool playerInRange = false;
    private Collider2D colliderComponent;

    private void Awake()
    {
        colliderComponent = GetComponent<Collider2D>();

        // Ensure the collider is a trigger to prevent physical collision response
        if (colliderComponent != null)
        {
            colliderComponent.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("ProximityText requires a Collider2D component to function.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the object has a "Player" tag
        {
            playerInRange = true;
            StartCoroutine(HideTextAfterDelay());
        }
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        if (playerInRange) // Ensure player is still in range before hiding
        {
            textObject.SetActive(false);
        }
    }
}
