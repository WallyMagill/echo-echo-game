using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject lockIn;

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies; // Different enemy types
        public int enemyCount; // Number of enemies in this wave
        public float spawnRate; // Delay between spawns
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;

    private bool startSpawning = false;
    private int currentWaveIndex = 0;
    private bool spawningWave = false;
    private int enemiesAlive = 0;

    private GameObject _player;
    private Collider2D _coll;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (startSpawning)
        {
            Debug.Log($"currentWaveIndex = {currentWaveIndex}");
            Debug.Log($"enemiesAlive = {enemiesAlive}");
            
            if (spawningWave || enemiesAlive > 0) return;
        
            if (currentWaveIndex < waves.Length)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                lockIn.SetActive(false);
            }
        }
    }

    IEnumerator StartNextWave()
    {
        spawningWave = true;
        yield return new WaitForSeconds(timeBetweenWaves);

        Wave wave = waves[currentWaveIndex];
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemies[Random.Range(0, wave.enemies.Length)]);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        currentWaveIndex++;
        spawningWave = false;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        GenEnemy enemyScript = enemy.GetComponent<GenEnemy>();
        if (enemyScript != null)
        {
            enemyScript.SetPlayerTransform(_player.transform);
            enemyScript.SetChaseNoticeDistance(99999);
        }

        enemiesAlive += 1;
        enemy.GetComponent<GenEnemy>().OnDeath += () => enemiesAlive -= 1;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("was player");
            // Calculate the exit direction
            Vector2 exitDir = (collision.transform.position - _coll.bounds.center).normalized;

            // If we exited from the top
            if (exitDir.y > 0)
            {
                Debug.Log("start");
                // Spawn enemies
                startSpawning = true;
                // Lock the gates
                lockIn.SetActive(true);
            }
        }
    }
}
