using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{
    public TextMeshProUGUI portalPrompt; // Assign your TextMeshPro UI text here
    public Animator portalAnimator; // Assign the Animator for the portal animation
    public string videoSceneName = "Video/VideoScene"; // Name of the scene with the video player
    public float animationDuration = 1.5f; // Duration of the portal animation in seconds

    private bool isPlayerInRange = false;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    private void Start()
    {
        // Get the SpriteRenderer component and store the original sorting order
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSortingOrder = spriteRenderer.sortingOrder;
        }

        // Ensure the portal prompt is hidden at the start
        if (portalPrompt != null)
        {
            portalPrompt.gameObject.SetActive(false); // Hide the prompt initially
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the portal collider
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (portalPrompt != null)
            {
                portalPrompt.gameObject.SetActive(true); // Show the prompt
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the portal collider
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (portalPrompt != null)
            {
                portalPrompt.gameObject.SetActive(false); // Hide the prompt
            }
        }
    }

    private void Update()
    {
        // Check if the player presses "E" while in the portal range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Hide the prompt and start the portal animation
            if (portalPrompt != null)
            {
                portalPrompt.gameObject.SetActive(false);
            }

            if (portalAnimator != null)
            {
                // Increase sorting order to bring portal to the top layer
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 1000; // Set a high sorting order
                }

                portalAnimator.speed = 0.25f; // Set animation speed to 50%
                portalAnimator.SetTrigger("PlayPortalAnimation"); // Trigger the portal animation
            }

            // Start the coroutine to switch scenes after the animation
            StartCoroutine(EnterPortalCoroutine());
        }
    }

    private IEnumerator EnterPortalCoroutine()
    {
        // Wait for the portal animation to finish
        yield return new WaitForSeconds(animationDuration); // Adjust this delay to match the length of the portal animation

        // Optional: Reset sorting order if needed (comment this out if not resetting)
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = originalSortingOrder;
        }

        // Switch to the scene with the video
        SceneManager.LoadScene(videoSceneName);
    }
}
