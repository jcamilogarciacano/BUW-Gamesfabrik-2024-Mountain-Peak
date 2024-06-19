using System.Collections;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public GameObject webPrefab; // Prefab for the web projectile
    public Transform player; // Reference to the player
    public float shootInterval = 2.0f; // Time between shots
    public float shootDuration = 5.0f; // Duration of the shooting phase
    public float waitTime = 3.0f; // Time to wait before respawning
    public Transform leftSpawnPoint; // Reference to the left spawn point
    public Transform rightSpawnPoint; // Reference to the right spawn point

    public float projectileSpeed = 5.0f; // Speed of the web projectile

    public enum SpiderState
    {
        Spawn,
        ShootWebs,
        Despawn
    }

    private SpiderState currentState;
    private Coroutine currentCoroutine;

    void Start()
    {
        // Debug.Log("SpiderController Start");
        currentState = SpiderState.Spawn;
        currentCoroutine = StartCoroutine(SpiderBehavior());
    }

    IEnumerator SpiderBehavior()
    {
        while (true)
        {
            switch (currentState)
            {
                case SpiderState.Spawn:
                    SpawnSpider();
                    break;
                case SpiderState.ShootWebs:
                    yield return StartCoroutine(ShootWebs());
                    break;
                case SpiderState.Despawn:
                    StartRespawnProcess();
                    yield break; // Exit the coroutine as the spider will be inactive
            }
            yield return null;
        }
    }

    void SpawnSpider()
    {
        // Debug.Log("Spider Spawning");
        gameObject.SetActive(true);
        currentState = SpiderState.ShootWebs;
    }

    IEnumerator ShootWebs()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < shootDuration)
        {
            ShootWeb();
            //MoveUpwards();
            yield return new WaitForSeconds(shootInterval);
            elapsedTime += shootInterval;
        }
        // Start coroutine to move upwards before despawning
        yield return StartCoroutine(MoveUpwardsBeforeDespawn());

        currentState = SpiderState.Despawn;

    }

    IEnumerator MoveUpwardsBeforeDespawn()
    {
        float moveDuration = 5.0f;
        float endTime = Time.time + moveDuration;
        float moveSpeed = 5.0f;

        while (Time.time < endTime)
        {
            // Move upwards
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            yield return null;
        }

        // Debug.Log("Spider finished moving upwards");
    }

    void ShootWeb()
    {
        // Logic for shooting webs
        float angle = Random.Range(-20f, 20f); // Add a bit of randomness to the angle
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right; // Change the direction based on the angle
        if (player.position.x < transform.position.x)
        {
            direction = -direction; // Reverse the direction if the player is on the left side
        }
        GameObject web = Instantiate(webPrefab, transform.position, Quaternion.identity);
        web.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed; // Adjust the speed as needed
    }

    // Method to initiate the respawn process
    void StartRespawnProcess()
    {
        // Debug.Log("Spider despawned");
        StartCoroutine(RespawnSpider());
    }
    IEnumerator RespawnSpider()
    {
        // Wait for the specified time before respawning
        yield return new WaitForSeconds(waitTime);

        // Determine spawn side (left or right) and position just below the screen
        bool isLeftSide = Random.Range(0, 2) == 0;
        Vector3 spawnPosition = isLeftSide ? leftSpawnPoint.position : rightSpawnPoint.position;
        spawnPosition.y -= 10; // Adjust to ensure it's below the screen
        spawnPosition.z = 1; // Set the z-coordinate explicitly to 1
        transform.position = spawnPosition;

        // Debug.Log("Spider Respawned at position: " + transform.position);

        // Set to spawn state and reactivate the spider
        currentState = SpiderState.Spawn;
        gameObject.SetActive(true);

        // Start the movement and behavior coroutine
        currentCoroutine = StartCoroutine(MoveAndShootWebs());
    }
    IEnumerator MoveAndShootWebs()
    {
        float targetY = Mathf.Max(leftSpawnPoint.position.y, rightSpawnPoint.position.y);
        while (transform.position.y < targetY)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5); // Adjust speed as necessary

            // Transition to ShootWebs state once after reaching a certain height
            if (currentState == SpiderState.Spawn)
            {
                currentState = SpiderState.ShootWebs;
                // Shoot webs logic here (consider moving this to a separate method or coroutine if complex)
            }

            yield return null;
        }

        // Ensure the spider stops at the correct height
        Vector3 position = transform.position;
        position.y = targetY;
        transform.position = position;

        // Log respawning for debugging purposes
        // Debug.Log("Spider Respawning");

        // Optionally, restart the main behavior coroutine or handle despawning
        // Ensure any ongoing coroutines are stopped before restarting
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(SpiderBehavior());
    }

}
