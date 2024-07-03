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

    public float shootTimer; // Timer for shooting
    public float waitTimer; // Timer for waiting

    public bool startShooting = false; // Whether the spider should start shooting

    public float rotationSpeed = 5f; // Example value, adjust as needed

    public float rotationThreshold = 1f; // Example value, adjust as needed
    public Animator animator;

    public AudioSource audioSource;
    public SpiderDetectionHelper detectionHelperCollider;

    // Start is called before the first frame update
    void Start()
    {
        //detectionHelperCollider = GetComponentInChildren<SpiderDetectionHelper>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootInterval; // Start shooting immediately
        waitTimer = 0.0f; // Start waiting immediately

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionHelperCollider != null)
        {
            startShooting = detectionHelperCollider.startShooting;
        }
        
        if (startShooting)
        {
            LookAtPlayer(); // Look at the player during shooting phase
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
                        //Call shoot web 3 times 0.5 seconds between each call
                        Invoke("ShootWeb", 0.0f);
                        //Invoke("ShootWeb", 0.5f);
                        //Invoke("ShootWeb", 0.5f);
                        //ShootWeb(); // Shoot web at the end of the shooting phase
                    }
                }
                else
                {
                    waitTimer = waitTime; // Start waiting after shooting phase
                }
            }
        }
        else
        {
         //   animator.SetBool("isRotating", false); // Set the animator bool to false if not rotating
           // audioSource.Stop();
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
        if (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold) // Using a threshold to check if rotation is significant
        {
            animator.SetBool("isRotating", true);
            //play one shot
            //audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            animator.SetBool("isRotating", false);
            //audioSource.Stop();
        }
    }

    // Shoot web function
    // Instantiate web prefab
    void ShootWeb()
    {
        float projectileSpeed = 10.0f; // Assign a value to projectileSpeed
        GameObject web = Instantiate(webPrefab, transform.position, Quaternion.identity);
        Vector3 direction = player.position - transform.position;
        //alter the direction vector a little with a random angle
        //direction = Quaternion.Euler(0, 0, Random.Range(-10, 10)) * direction;
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
            if (detectionHelperCollider == null)
            {
                startShooting = true;
            }
        }
    }
    //ontrigger exit2d
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (detectionHelperCollider == null)
            {
                startShooting = false;
            }
        }
    }

}
