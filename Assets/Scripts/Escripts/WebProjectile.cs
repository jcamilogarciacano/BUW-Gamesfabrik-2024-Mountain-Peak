using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    // public GameObject webSplash;

    // private const float SPEED = 10f; // Speed of the projectile
    public float speed = 10f; // Speed of the web projectile
    public Rigidbody2D rb; // Rigidbody of the web projectile
    public GameObject webSplashPrefab; // Prefab for the web splash effect

    public float webSplashDuration = 5f; // Duration of the web splash effect

    // private Vector3 dir; // Direction of the projectile

    void Start()
    {
        //delete the webprojectile after 5 seconds
        Destroy(gameObject, 10f);
        //rotate the prefab 360 degrees continuously
        // Assuming the web is shot straight in the direction it's facing
        //rb.velocity = transform.right * speed;
    }

    // This method allows for dynamic direction setting if needed
    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Example: Instantiate web splash effect on collision
        if (webSplashPrefab != null)
        {
            //instanciate the websplasprefab at the position and the rotation of the webprojectile
            GameObject splashInstance = Instantiate(webSplashPrefab, transform.position, transform.rotation);
            //destroy the splashInstance after 5 seconds
            Destroy(splashInstance, webSplashDuration);
        }

        // Add collision handling logic here
        // Destroy the projectile or apply effects

        Destroy(gameObject); // Destroy the projectile upon collision
    }

    // void OnTriggerEnter2D(Collider2D hitInfo)
    // {
    //     // Instantiate a splash effect at the collision point
    //     GameObject splashInstance = Instantiate(webSplashPrefab, hitInfo.gameObject.transform.position, Quaternion.identity);
    //     // Destroy the projectile and the splash effect after 5 seconds
    //     Destroy(splashInstance, 5f);
    //     // Check if it hits an enemy or an environment object
    //     if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Environment"))
    //     {
    //         // Instantiate the web splash effect at the point of collision
    //         Instantiate(webSplashPrefab, transform.position, transform.rotation);

    //         // Optionally, apply effects to the hit object
    //         // For example, if it's an enemy, you might want to freeze them

    //         // Destroy the web projectile
    //         Destroy(gameObject);
    //     }
    // }
    // private void Awake()
    // {
    //     Setup(new Vector3(1, 0));
    // }

    // private void Setup(Vector3 dir)
    // {
    //     this.dir = dir;
    // }

     private void Update()
     {
//transform.position += SetDirectionToPlayer() * SPEED * Time.deltaTime;
//rotate the prefab 1  degree per second continuously
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 360);
     }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     //Debug.Log("Web collided with: " + collision.gameObject.name);
    //     // checking if there is a collision with the player
    //     if (collision.CompareTag("Player"))
    //     {
    //         // Make the player stick to the splashInstance
    //         // collision.transform.parent = transform;
    //         // Freeze the player
    //         FreezePlayer(collision.gameObject);
    //     }
    //     // Instantiate a splash effect at the collision point
    //     GameObject splashInstance = Instantiate(webSplash, collision.gameObject.transform.position, Quaternion.identity);
    //     // Destroy the projectile and the splash effect after 5 seconds
    //     // Destroy(gameObject);
    //     Destroy(splashInstance, 5f);
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         // Unparent the player when they leave the splashInstance
    //         // collision.transform.parent = null;
    //         // Unfreeze the player
    //     UnfreezePlayer(collision.gameObject);
    //     }
    // }

    // New methods to freeze and unfreeze the player

    // private void FreezePlayer(GameObject player)
    // {
    //     // Assuming the player has a script named 'PlayerMovement' that controls movement
    //     Movement2 playerMovement = player.GetComponent<Movement2>();
    //     if (playerMovement != null)
    //     {
    //         playerMovement.enabled = false;
    //     }
    //     else
    //     {
    //         Debug.LogError("PlayerMovement script not found on the player");
    //     }
    // }

    // private void UnfreezePlayer(GameObject player)
    // {
    //     Movement2 playerMovement = player.GetComponent<Movement2>();
    //     if (playerMovement != null)
    //     {
    //         playerMovement.enabled = true;
    //     }
    //     else
    //     {
    //         Debug.LogError("PlayerMovement script not found on the player");
    //     }
    // }

    // public Vector3 SetDirectionToPlayer()
    // {
    //     GameObject player = GameObject.FindWithTag("Player"); // Find the player GameObject by tag.
    //     if (player != null)
    //     {
    //         Vector3 playerPosition = player.transform.position;
    //         Vector3 spiderPosition = transform.position;
    //         Vector3 directionToPlayer = playerPosition - spiderPosition;
    //         dir = directionToPlayer.normalized; // Assuming 'dir' is a Vector3 field in this class.
    //     }
    //     else
    //     {
    //         Debug.LogError("Player not found");
    //     }
    //     return dir;
    // }

}
