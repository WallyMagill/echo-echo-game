using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerComponent : MonoBehaviour
{
    public float speed = 5f;  // Default speed
    private Vector2 direction = Vector2.right;  // Default direction

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float Speed
    {
        get { return speed; }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;  // Normalize to ensure consistent movement
    }

    public Vector2 Direction
    {
        get { return direction; }
    }

    private void Update()
    {
        // Move the enemy in the set direction at the current speed
        transform.position = (Vector2)transform.position + (direction * speed * Time.deltaTime);
    }
}
