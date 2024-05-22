using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour
{
    public GameObject giantPrefab;
    public Transform characterTransform;
    public GameObject rockPrefab;
    public float throwForce = 10f;
    public float spawnDistance = 10f;
    
    public int maxRocks = 6;

    private void Start()
    {
        InvokeRepeating("SpawnGiant", 15f, 15f);
    }
    private void SpawnGiant()
    {
        // Calculate the spawn position for the giant
        Vector3 spawnPosition;

        int randomOption = Random.Range(0, 6);

        switch (randomOption)
        {
            case 0:
            // Option 1: Negative spawn position
            spawnPosition = characterTransform.position - (characterTransform.right * spawnDistance);
            break;
            case 1:
            // Option 2: Positive spawn position
            spawnPosition = characterTransform.position + (characterTransform.right * spawnDistance);
            break;
            case 2:
            // Option 3: Top left corner
            spawnPosition = characterTransform.position + (characterTransform.up * spawnDistance) - (characterTransform.right * spawnDistance);
            break;
            case 3:
            // Option 4: Top right corner
             spawnPosition = characterTransform.position + (characterTransform.up * spawnDistance) + (characterTransform.right * spawnDistance);
            break;
            case 4:
            // Option 5: Top middle position
            spawnPosition = characterTransform.position + (characterTransform.up * spawnDistance*1.5f);
            break;
            case 5:
            // Option 6: Top middle position again
            spawnPosition = characterTransform.position + (characterTransform.up * spawnDistance*1.5f);
            break;
            default:
            // Default: Negative spawn position
            spawnPosition = characterTransform.position - (characterTransform.right * spawnDistance);
            break;
        }
        // Instantiate the giant prefab at the spawn position
        GameObject giant = Instantiate(giantPrefab, spawnPosition, Quaternion.identity);
        // Throw rocks at the giant

        // Start the coroutine to throw rocks at the giant
        StartCoroutine(ThrowRocksCoroutine(giant));
        //destroy giant instanciated
        Destroy(giant, 5f);
    }

    private IEnumerator ThrowRocksCoroutine(GameObject target)
    {
        for (int i = 0; i < maxRocks; i++)
        {
            // Instantiate the rock prefab
            GameObject rock = Instantiate(rockPrefab, target.transform.position, Quaternion.identity);

            // Calculate the direction towards the player
            Vector2 direction = (characterTransform.position - rock.transform.position).normalized;

            // Add a little randomness to the direction
            float randomAngle = Random.Range(-30f, 30f);
            direction = Quaternion.Euler(0f, 0f, randomAngle) * direction;

            // Get the Rigidbody component of the rock
            Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();

            // Apply a force to the rock in the direction of the player
            rockRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);

            // Destroy the rock after 3 seconds
            Destroy(rock, 3f);

            // Wait for 0.1 seconds before throwing the next rock
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void ThrowRocks(GameObject target)
    {
        // Instantiate the rock prefab
        GameObject rock = Instantiate(rockPrefab, target.transform.position, Quaternion.identity);

        // Calculate the direction towards the player
        Vector2 direction = (characterTransform.position - rock.transform.position).normalized;

        // Get the Rigidbody component of the rock
        Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();

        // Apply a force to the rock in the direction of the player
        rockRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // Destroy the rock after 3 seconds
        Destroy(rock, 3f);
    }
}
