using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's transform
    public float smoothSpeed = 0.125f; // Smoothing speed
    public Vector3 offset; // Offset for the camera position
    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D
    public float followThreshold = 0.5f; // Start following when above this threshold

    private float highestPoint = 0f; // Tracks the highest point the camera has reached

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the player
            highestPoint = transform.position.y; // Initialize the highest point as the starting camera position
        }
    }

    void LateUpdate()
    {
        if (player != null && playerRb != null)
        {
            // Check if the player is above the current highest point
            if (player.position.y > highestPoint)
            {
                highestPoint = player.position.y; // Update the highest point
            }

            // Follow the player only if they are moving upward or above the highest point
            if (player.position.y > highestPoint - followThreshold)
            {
                Vector3 desiredPosition = new Vector3(
                    transform.position.x,
                    player.position.y + offset.y,
                    transform.position.z
                );
                transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            }
        }
    }
}
