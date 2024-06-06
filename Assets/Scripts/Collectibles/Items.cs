using UnityEngine;

public class Items : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.name); // Log the name of the colliding object

        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Player"); // Log when collision with Player is detected

            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                Debug.Log("PlayerController found, collecting item"); // Log when PlayerController is found
                player.CollectItem();
                Debug.Log("Destroying the collectible item"); // Log before destroying the item
                Destroy(gameObject); // This should destroy the collectible item
            }
            else
            {
                Debug.Log("PlayerController component not found on Player object");
            }
        }
        else
        {
            Debug.Log("The collided object does not have the 'Player' tag");
        }
    }
}
