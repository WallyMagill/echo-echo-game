using UnityEngine;

public class Pickup : MonoBehaviour
{
    public WeaponData weaponData; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Inventory inventory = other.GetComponent<Inventory>();
            int val = inventory.AddWeapon(weaponData);
            if ( val >= 0) {
                Destroy(gameObject);
            }
        }
    }
}

