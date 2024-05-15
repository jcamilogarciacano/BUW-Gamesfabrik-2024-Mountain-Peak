using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The force applied to the player for jumping
    public float jumpForce = 5.0f;

    // The multiplier for movement speed
    public float movementSpeedMultiplier = 1.0f;

    // The duration of the jump force
    public float jumpDuration = 1.0f;

    // The gravity multiplier
    public float gravityMultiplier = 1.0f;

    // The timer for the jump duration
    private float jumpTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Get input for vertical movement
        float moveVertical = Input.GetAxis("Vertical");

        // if jectpack is enabled, double move horizotanl movement speed
        if (GetComponent<JetpackController>().enabled == false)
        {
            moveHorizontal *= 2;
        }
        //do the else
        else
        {
            moveHorizontal *= 1;
        }
        // Calculate the movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * movementSpeedMultiplier;

        // Get the Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply the movement to the Rigidbody2D
        rb.velocity = movement;

        // Apply gravity with the gravity multiplier
        rb.AddForce(new Vector2(0, Physics2D.gravity.y * gravityMultiplier));

        // Check if the player should jump
        if (GetComponent<JetpackController>().enabled == false && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
        {

            // Start the jump timer
            jumpTimer = jumpDuration;
            // Apply an upward force to the Rigidbody2D for jumping
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
        // Update the jump timer
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump"))
        {
        
            if (jumpTimer > 0)
            {   
                
                jumpTimer -= Time.deltaTime;
                // Apply the jump force for the duration of the jump timer
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
        }
        if (GetComponent<JetpackController>().enabled == false)
        {
            //change the gravity to 25f
            gravityMultiplier = 25f;
            movementSpeedMultiplier = 6.0f;
        }
        else if (GetComponent<JetpackController>().enabled == true)
        {
            //change the gravity to 15f
            gravityMultiplier = 15f;
            movementSpeedMultiplier = 8.0f;
        }
    }
}
