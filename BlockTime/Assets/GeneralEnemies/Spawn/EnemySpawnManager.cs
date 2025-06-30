using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    [SerializeField]
    private LayerMask _layersEnemyCannotSpawnOn;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnEnemies(Collider2D spawnableAreaCollider, GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);
            if (spawnPosition != Vector2.zero)
            {
                GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

                GenEnemy enemyScript = spawnedEnemy.GetComponent<GenEnemy>();
                if (enemyScript != null)
                {
                    enemyScript.SetPlayerTransform(_player.transform);
                }
            }
        }
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int maxAttempts = 200;

        while(!isSpawnPosValid && attemptCount < maxAttempts)
        {
            spawnPosition = GetRandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f); // <- hardcoded and manipulatable

            bool isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & _layersEnemyCannotSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            Debug.LogWarning("Could not find a valid spawn position.");
            return Vector2.zero;
        }

        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 0.5f)
    {
        Bounds collBounds = collider.bounds;

        float clampedOffsetX = Mathf.Min(offset, (collBounds.size.x / 2));
        float clampedOffsetY = Mathf.Min(offset, (collBounds.size.y / 2));

        Vector2 minBounds = new Vector2(collBounds.min.x + clampedOffsetX, collBounds.min.y + clampedOffsetY);
        Vector2 maxBounds = new Vector2(collBounds.max.x - clampedOffsetX, collBounds.max.y - clampedOffsetY);

        float randomx = Random.Range(minBounds.x, maxBounds.x);
        float randomy = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomx, randomy);
    }
}
