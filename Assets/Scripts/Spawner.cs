using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject bricksToEnable;
    [SerializeField] private GameObject bricksToDisable;

    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("spane");
            // Enable the object and disable the other
            bricksToEnable.SetActive(true);
            bricksToDisable.SetActive(false);

            // Move the disabled object 30 units in the Y direction
            MoveObjectAbove(bricksToDisable);
            gameManager.IncreaseScore();
            
        }
    }

    // Move the object 30 units above its current position
    void MoveObjectAbove(GameObject obj)
    {
        if (obj != null)
        {
            // Calculate the new position 30 units above the current Y position
            Vector3 newPosition = new Vector3(Random.Range(-2f,-4f), obj.transform.position.y + 30f, obj.transform.localPosition.z);

            // Set the new position
            obj.transform.position = newPosition;

            // Optionally, re-enable the object after moving it
            //obj.SetActive(true);

            // Debug log to confirm the move
            Debug.Log($"Moved {obj.name} to new position: {newPosition}");
        }
    }
}
