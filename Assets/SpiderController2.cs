using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController2 : MonoBehaviour
{
    public GameObject webPrefab; // Prefab for the web projectile
    public Transform player; // Reference to the player
    public float shootInterval = 2.0f; // Time between shots
    public float shootDuration = 5.0f; // Duration of the shooting phase
    public float waitTime = 3.0f; // Time to wait before respawning

    private float shootTimer; // Timer for shooting
    private float waitTimer; // Timer for waiting

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootInterval; // Start shooting immediately
        waitTimer = 0.0f; // Start waiting immediately
    }

    // Update is called once per frame
    void Update()
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
                LookAtPlayer(); // Look at the player during shooting phase
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

    // Look at player function
    // Rotate on axis to look at player
    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
}
