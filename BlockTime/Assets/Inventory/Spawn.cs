// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Spawn : MonoBehaviour
// {
//     public GameObject item;  
//     public GameObject itemButtonPrefab;  
//     private Transform player;

//     private void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     public void SpawnDroppedItem()
//     {
        
//         Vector2 playerPos = new Vector2(player.position.x, player.position.y + 2);
//         GameObject droppedItem = Instantiate(item, playerPos, Quaternion.identity);

//         Collider2D collider = droppedItem.GetComponent<Collider2D>();
//         if (collider == null)
//         {
//             collider = droppedItem.AddComponent<BoxCollider2D>();
//         }

//         collider.isTrigger = true;

//         Pickup pickupScript = droppedItem.AddComponent<Pickup>();
//         pickupScript.itemButton = itemButtonPrefab;
//     }
// }

