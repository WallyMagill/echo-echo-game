using System.Collections.Generic;
using UnityEngine;

public class ItemExchangeLocation : MonoBehaviour
{
    [System.Serializable]
    public struct ExchangePair
    {
        public WeaponData requiredItem;
        public WeaponData rewardItem;
    }

    public List<ExchangePair> exchangePairs;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<Inventory>(); // Find the player's inventory
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AttemptExchange();
        }
    }

    private void AttemptExchange()
    {
        foreach (var pair in exchangePairs)
        {
            int slotIndex = FindItemInInventory(pair.requiredItem);
            if (slotIndex != -1)
            {
                // Drop the item in the world instead of just removing it
                playerInventory.RemoveWeapon(slotIndex);

                // Add the new item
                playerInventory.AddWeapon(pair.rewardItem);
                
                Debug.Log($"Exchanged {pair.requiredItem.name} for {pair.rewardItem.name}");
                return;
            }
        }
        Debug.Log("No valid item to exchange!");
    }


    private int FindItemInInventory(WeaponData item)
    {
        for (int i = 0; i < 2; i++) // Assuming 2 inventory slots
        {
            if (playerInventory.GetWeaponInSlot(i) == item) // Compare weapon data directly
            {
                return i;
            }
        }
        return -1;
    }
}
