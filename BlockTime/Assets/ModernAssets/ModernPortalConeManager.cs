using UnityEngine;

public class ModernPortalConeManager : MonoBehaviour
{
    public GameObject[] cones; // Assign cone GameObjects in the Inspector
    public WeaponData timeCrystal; // Assign the TimeCrystal WeaponData in the Inspector

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (playerInventory != null && playerInventory.HasItem(timeCrystal))
        {
            DestroyCones();
        }
    }

    private void DestroyCones()
    {
        foreach (GameObject cone in cones)
        {
            if (cone != null)
            {
                Destroy(cone);
            }
        }
        Debug.Log("Cones removed! Portal is now accessible.");
        enabled = false; // Disable this script to stop checking
    }
}

