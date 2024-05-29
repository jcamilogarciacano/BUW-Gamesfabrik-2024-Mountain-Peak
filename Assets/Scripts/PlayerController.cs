using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*So the list of anims would be (in order or priority)

1. Iddle (the character just standing still waiting for a command)
2. Walking ( or Running , do both if enough time) 
3. Jumping
4. Dashing to a side ( or sliding to a side) 
5. Jetpack beign used ( some smoke effect or smth) 
6. Rocket ( or maybe what it does is , inverse the gravity of the character? )
*/

public class PlayerController : MonoBehaviour
{
    // The multiplier for movement speed
    public float movementSpeedMultiplier = 1.0f;

    //check if grounded
    public bool isGrounded = false;
    public DoodleJumper doodleJumper;

    // Start is called before the first frame update
    void Start()
    {
        // Get the DoodleJumper script component
        doodleJumper = GetComponent<DoodleJumper>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Get input for vertical movement
        //float moveVertical = Input.GetAxis("Vertical");

        // if jectpack is enabled, double move horizotanl movement speed
/*         if (GetComponent<JetpackController>().enabled == false)
        {
            moveHorizontal *= 2;
        }
        //do the else
        else
        {
            moveHorizontal *= 1;
        } */
        // Calculate the movement vector
        Vector2 movement = new Vector2(moveHorizontal * movementSpeedMultiplier, 0);

        // Get the Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply the movement to the Rigidbody2D
        rb.velocity = movement;
        if (isGrounded)
        {
            // Set the "canJump" variable in the DoodleJumper script to true
        
            if (doodleJumper != null)
            {
                doodleJumper.canJump = true;
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
