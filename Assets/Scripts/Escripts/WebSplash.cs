using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSplash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //i want to check if a gameobject with tag player enters the trigger of the web splash and if so, freeze its position for 5 seconds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Make the player stick to the splashInstance
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            // Freeze the player
            FreezePlayer(collision.gameObject);
            // Unfreeze the player after 5 seconds
            StartCoroutine(UnfreezePlayerAfterDelay(collision.gameObject, 5f));
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Unparent the player when they leave the splashInstance
            //collision.transform.parent = null;
            // Unfreeze the player
            UnfreezePlayer(collision.gameObject);
        }
    }
    
    private void FreezePlayer(GameObject player)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void UnfreezePlayer(GameObject player)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
    private IEnumerator UnfreezePlayerAfterDelay(GameObject player, float delay)
    {
        yield return new WaitForSeconds(delay);
        UnfreezePlayer(player);
    }
}
