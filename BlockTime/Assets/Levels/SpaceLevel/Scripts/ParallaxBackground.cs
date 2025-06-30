using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Vector2 parallaxFactor; // How much the background should move (X, Y)

    private Vector3 lastPlayerPosition; // Track the player's last position

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        // Initialize the last player position
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Calculate the player's movement
        Vector3 playerDelta = player.position - lastPlayerPosition;

        // Apply a fraction of the player's movement to the background
        transform.position += new Vector3(playerDelta.x * parallaxFactor.x, playerDelta.y * parallaxFactor.y, 0);

        // Update the last player position
        lastPlayerPosition = player.position;
    }
}
