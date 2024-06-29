using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController2 : MonoBehaviour
{
    public GameObject webPrefab; // Prefab for the web projectile
    public Transform player; // Reference to the player
    public float shootInterval = 2.0f; // Time between shots
    //public float shootDuration = 5.0f; // Duration of the shooting phase
    public float waitTime = 3.0f; // Time to wait before respawning

    private float shootTimer; // Timer for shooting
    private float waitTimer; // Timer for waiting

    public bool startShooting = false; // Whether the spider should start shooting

    public float rotationSpeed = 5f; // Example value, adjust as needed
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootInterval; // Start shooting immediately
        waitTimer = 0.0f; // Start waiting immediately
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer(); // Look at the player during shooting phase
        if (startShooting)
        {
            if (waitTimer > 0.0f)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0.0f)
                {
                    shootTimer = shootInterval; // Reset shoot timer after waiting
                }
            }
            else
            {
                if (shootTimer > 0.0f)
                {
                    shootTimer -= Time.deltaTime;

                    if (shootTimer <= 0.0f)
                    {
                        ShootWeb(); // Shoot web at the end of the shooting phase
                    }
                }
                else
                {
                    waitTimer = waitTime; // Start waiting after shooting phase
                }
            }
        }
    }

    // Look at player function
    // Rotate on axis to look at player
    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        // Calculate the angle between the spider and the player in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Create a target rotation based on the angle, rotating around the Z-axis
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 if your sprite is facing up by default

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Check if the spider is changing the rotation and set the animator bool to true
        // This part might need adjustment based on your specific requirements
        if (Quaternion.Angle(transform.rotation, targetRotation) > 1) // Using a threshold to check if rotation is significant
        {
            animator.SetBool("isRotating", true);
        }
        else
        {
            animator.SetBool("isRotating", false);
        }
    }

    // Shoot web function
    // Instantiate web prefab
    void ShootWeb()
    {
        float projectileSpeed = 10.0f; // Assign a value to projectileSpeed
        GameObject web = Instantiate(webPrefab, transform.position, Quaternion.identity);
        Vector3 direction = player.position - transform.position;
        web.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

        // Rotate the web towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        web.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Add an OnTrigger2DEnter method to detect when the player is inside the shooting zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startShooting = true;

        }
    }
    //ontrigger exit2d
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startShooting = false;
        }
    }

}
