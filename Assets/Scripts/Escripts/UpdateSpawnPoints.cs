using UnityEngine;

public class UpdateSpawnPoints : MonoBehaviour
{
    public Transform leftSpawnPoint; // Reference to the left spawn point
    public Transform rightSpawnPoint; // Reference to the right spawn point

    void Update()
    {
        // Update the y-coordinate of the spawn points to match the camera's y-coordinate
        float cameraY = Camera.main.transform.position.y;

        // Update left spawn point
        Vector3 leftPosition = leftSpawnPoint.position;
        leftPosition.y = cameraY;
        leftSpawnPoint.position = leftPosition;

        // Update right spawn point
        Vector3 rightPosition = rightSpawnPoint.position;
        rightPosition.y = cameraY;
        rightSpawnPoint.position = rightPosition;
    }
}
