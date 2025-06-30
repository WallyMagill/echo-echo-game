using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    private HurtboxComponent hurtbox;

    void Start()
    {
        // Get the HurtboxComponent from the child object
        hurtbox = GetComponentInChildren<HurtboxComponent>();

        if (hurtbox == null)
        {
            Debug.LogError("PlayerDeathHandler could not find HurtboxComponent! Make sure it's attached to the Hurtbox child.");
        }
    }

    void Update()
    {
        if (hurtbox != null && hurtbox.Health <= 0) // Check if Player is dead
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        Debug.Log("Player has died! Returning to Main Menu...");
        SceneManager.LoadScene("Levels/DeathLevel/DeathLevel"); // Adjust based on Build Settings
    }
}


