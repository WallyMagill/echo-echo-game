using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject enemyCarPrefab; 
    public Transform spawnPoint; 
    public bool spawnsDown = true; 
    public float spawnInterval = 2f; 

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
{
    while (gameObject.activeInHierarchy) 
    {
        SpawnCar();
        yield return new WaitForSeconds(spawnInterval);
    }
}


    void SpawnCar()
{
    GameObject newCar = Instantiate(enemyCarPrefab, spawnPoint.position, Quaternion.identity);
    //Debug.Log("Spawned Car: " + newCar.name);

    CarEnemyMovement carScript = newCar.GetComponent<CarEnemyMovement>();
    if (carScript == null)
    {
        Debug.LogError("CarEnemyMovement script is missing from " + newCar.name);
        return;
    }

    // Set the car’s movement direction based on spawnsDown
    carScript.movesDown = spawnsDown;

    FireProjectilesComponent fireComponent = newCar.GetComponent<FireProjectilesComponent>();
    if (fireComponent != null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            fireComponent.target = player.transform; 
        }
        else
        {
            Debug.LogError("Player not found! Make sure the Player has the correct tag.");
        }
    }
}



}

