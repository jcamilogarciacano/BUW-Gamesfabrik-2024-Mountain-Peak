using UnityEngine;

public class GiantActions : MonoBehaviour
{
    public GameObject rockPrefab; // Prefab for the rock projectile
    public Transform player; // Reference to the player
    public Transform leftPoint; // Reference to the left point
    public Transform rightPoint; // Reference to the right point
    public float detectionRadius = 20.0f; // Radius within which the giant detects the player
    public float throwInterval = 2.0f; // Interval between throws
    public float throwForce = 10.0f; // Force with which the rock is thrown
    public Vector3 throwOffset; // Offset from the giant's position where the rock is spawned

    public int numberOfRocks = 3; // Number of rocks to throw
    public Vector2 rockSizeRange = new Vector2(0.5f, 1.5f); // Range of rock sizes

    private bool isAwake = false; // Whether the giant is awake
    private bool isVisible = true; // Whether the giant is currently visible
    private float nextThrowTime = 0.0f; // Time when the giant can throw the next rock

    void Awake()
    {
        //use gameobject .find to find the objecst with tag "Player", "LeftPoint" and "RightPoint"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftPoint = GameObject.FindGameObjectWithTag("Left").transform;
        rightPoint = GameObject.FindGameObjectWithTag("Right").transform;
    }
    // void Update()
    // {
    //     // Check the distance to the player
    //     float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    //     float distanceToLeft = Vector3.Distance(transform.position, leftPoint.position);
    //     float distanceToRight = Vector3.Distance(transform.position, rightPoint.position);

    //     bool isPlayerNear = distanceToPlayer <= detectionRadius;
    //     bool isLeftNear = distanceToLeft <= detectionRadius;
    //     bool isRightNear = distanceToRight <= detectionRadius;

    //     if (isPlayerNear)
    //     {
    //         if (!isAwake)
    //         {
    //             WakeUp();
    //         }

    //         // If the giant is awake, throw rocks at intervals
    //         if (isAwake && Time.time >= nextThrowTime)
    //         {
    //             ThrowRocks();
    //             nextThrowTime = Time.time + throwInterval;
    //         }
    //     }

    //     if (isLeftNear || isRightNear)
    //     {
    //         if (!isAwake)
    //         {
    //             WakeUp();
    //         }
    //         if (!isVisible)
    //         {
    //             Debug.Log("Inside isVisible");
    //             Appear();
    //         }
    //     }
    //     else if (!isPlayerNear && isAwake)
    //     {
    //         GoToSleep();
    //         Disappear();
    //     }
    // }

    void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            if (!isAwake)
            {
                WakeUp();
            }

            // If the giant is awake, throw rocks at intervals
            if (isAwake && Time.time >= nextThrowTime)
            {
                ThrowRocks();
                nextThrowTime = Time.time + throwInterval;
            }
        }
        else if (isAwake)
        {
            GoToSleep();
        }
    }
    

    void WakeUp()
    {
        isAwake = true;
        // Add any additional logic for waking up (e.g., animations, sounds)
        Debug.Log("Giant has woken up!");
        // gameObject.SetActive(true); // Ensure the giant is active when waking up
    }

    void GoToSleep()
    {
        isAwake = false;
        // Add any additional logic for going to sleep (e.g., animations, sounds)
        Debug.Log("Giant has gone to sleep!");
    }

    void Disappear()
    {
        // Add logic to make the giant disappear
        gameObject.SetActive(false);
        isVisible = false;
        Debug.Log("Giant has disappeared!");
    }

    void Appear()
    {
        // Add logic to make the giant appear
        gameObject.SetActive(true);
        isVisible = true;
        Debug.Log("Giant has appeared!");
    }

    void ThrowRocks()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            // Instantiate the rock prefab at the offset position
            GameObject rock = Instantiate(rockPrefab, transform.position + throwOffset, Quaternion.identity);

            // Destroy the rock after 5 seconds
            Destroy(rock, 5f);

            // Randomize the rock size
            float rockSize = Random.Range(rockSizeRange.x, rockSizeRange.y);
            rock.transform.localScale = new Vector3(rockSize, rockSize, rockSize);

            // Calculate the direction to the player with some spread
            Vector3 direction = (player.position - transform.position).normalized;
            float spreadAngle = Random.Range(-15f, 15f);
            direction = Quaternion.Euler(0, 0, spreadAngle) * direction;

            // Apply force to the rock's Rigidbody to throw it towards the player
            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }

        Debug.Log("Giant throws rocks!");
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the scene view for debugging purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
