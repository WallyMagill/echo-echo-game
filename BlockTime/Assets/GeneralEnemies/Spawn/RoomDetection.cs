using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    public bool AllowUpExit = true;
    public bool AllowDownExit = false;
    public bool AllowLeftExit = false;
    public bool AllowRightExit = false;

    [SerializeField]
    private GameObject[] _enemiesToSpawnIn;

    [SerializeField]
    private Collider2D _currentRoomSpawableArea;

    private GameObject _player;
    private Collider2D _coll;

    private Dictionary<GameObject, Vector2> _entryPositions = new Dictionary<GameObject, Vector2>();

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Store the position where the player entered
            _entryPositions[collision.gameObject] = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _entryPositions.ContainsKey(collision.gameObject))
        {
            Vector2 entryPos = _entryPositions[collision.gameObject];

            // Calculate the entrance and exit direction
            Vector2 enterDir = (entryPos - (Vector2)_coll.bounds.center).normalized;
            Vector2 exitDir = (collision.transform.position - _coll.bounds.center).normalized;

            if ((AllowUpExit && exitDir.y > 0 && enterDir.y < 0) ||
                (AllowDownExit && exitDir.y < 0 && enterDir.y > 0) ||
                (AllowLeftExit && exitDir.x < 0 && enterDir.x > 0) ||
                (AllowRightExit && exitDir.x > 0 && enterDir.x < 0))
            {
                if (!AreEnemiesPresent()){
                    EnemySpawnManager.instance.SpawnEnemies(_currentRoomSpawableArea, _enemiesToSpawnIn);
                }
            }
        }

        _entryPositions.Remove(collision.gameObject);
    }

    private bool AreEnemiesPresent()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_currentRoomSpawableArea.bounds.center, _currentRoomSpawableArea.bounds.size, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }
}