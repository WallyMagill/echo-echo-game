using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public GameObject[] rocks;  // Array to store all rock objects
    public HurtboxComponent dragonHurtbox; // Reference to Dragon's health component

    void Update()
    {
        if (dragonHurtbox != null && dragonHurtbox.Health <= 0) // Check if Dragon is dead
        {
            RemoveRocks();
        }
    }

    void RemoveRocks()
    {
        foreach (GameObject rock in rocks)
        {
            if (rock != null)
            {
                Destroy(rock);  // Destroy each rock
            }
        }
        Destroy(gameObject); // Destroy RockManager after removing rocks
    }
}
