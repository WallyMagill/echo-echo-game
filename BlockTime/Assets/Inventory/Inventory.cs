using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Inventory UI slots
    public GameObject[] slotUI;
    
    // Weapon parent transform to instantiate weapons
    public WeaponParent weaponParent;

    private WeaponData[] weapons = {null, null};
    private GameObject currentWeapon;
    private int currentIndex = 0;

    public int AddWeapon(WeaponData weapon)
    {
        if (weapon == null) return -1;
        for (int i = 0; i < weapons.Length; i++)
        {
            // Find first empty slot in the inventory
            if (weapons[i] == null)
            {
                // Set the weapon in the inventory
                weapons[i] = weapon;

                // Set the sprite icon in the inventory UI
                Image slotRenderer = slotUI[i].transform.GetChild(0).GetComponent<Image>();
                slotRenderer.color = Color.white;
                slotRenderer.sprite = weapon.weaponIcon; // should be setting the sprite of the child, not the parent
                slotRenderer.type = Image.Type.Simple;
                slotRenderer.preserveAspect = true;


                // Equip the weapon if in the current slot
                if (i == currentIndex) EquipWeapon(i);
                return 0;
            }
        }
        return -1;
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length || (index == currentIndex && currentWeapon != null)) return;
        
        // Destroy the current weapon
        if (currentWeapon != null) 
        {
            Destroy(currentWeapon);
        }
        
        // Change the index
        currentIndex = index;
        
        // Create the new weapon if possible
        if (weapons[index] != null)
        {
            currentWeapon = Instantiate(weapons[index].weaponPrefab, weaponParent.transform, false);
            weaponParent.Equip(currentWeapon);
        }
        // Otherwise set to empty
        else
        {
            currentWeapon = null;
        }
    }

    public void DropItem()
{
    if (currentWeapon == null) return;

    // Instantiate the pickup at the drop location with an offset
    Vector3 offset = new Vector3(1, 0, 0);
    Instantiate(weapons[currentIndex].pickupPrefab, weaponParent.transform.position + offset, quaternion.identity, null);
    
    // Remove the weapon from the inventory
    weapons[currentIndex] = null;
    
    // Destroy the held weapon
    Destroy(currentWeapon);
    currentWeapon = null;
    
    // Remove the weapon from the UI (fix: get child image instead of parent)
    Image slotRenderer = slotUI[currentIndex].transform.GetChild(0).GetComponent<Image>();
    slotRenderer.color = Color.clear;
    slotRenderer.sprite = null;
}

//methods for modern item exchange
    public void RemoveWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length || weapons[index] == null) return;

        // Drop the item in the world
        Vector3 dropPosition = weaponParent.transform.position + new Vector3(1, 0, 0); // Offset to avoid overlap
        Instantiate(weapons[index].pickupPrefab, dropPosition, Quaternion.identity);

        // Clear the inventory slot
        weapons[index] = null;

        // Update the UI slot
        Image slotRenderer = slotUI[index].transform.GetChild(0).GetComponent<Image>();
        slotRenderer.color = Color.clear;
        slotRenderer.sprite = null;
    }


    public WeaponData GetWeaponInSlot(int index)
    {
        if (index < 0 || index >= weapons.Length) return null;
        return weapons[index];
    }

    public bool HasItem(WeaponData item)
    {
        foreach (var weapon in weapons)
        {
            if (weapon != null && weapon == item)
            {
                return true; // Found the item
            }
        }
        return false;
    }




}