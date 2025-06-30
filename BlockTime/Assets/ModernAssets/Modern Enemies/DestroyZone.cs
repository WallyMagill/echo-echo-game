using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision detected for enemy");
        if (other.CompareTag("ModernEnemyCar")) 
        {
            Destroy(other.gameObject);
            Debug.Log("car destoryed");
        }
    }
}
