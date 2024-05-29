using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleJumper : MonoBehaviour
{
    public float jumpForce = 5f; // Force of the jump
    private Rigidbody2D rb;
    private bool jumpPeakReached = false; // Flag to track if the character has reached the peak of the jump
    public float originalGravityScale; // Store the original gravity scale

    public bool canJump = false; // Flag to track if the player can jump

    private GrabbingController grabbingController; // Reference to the GrabbingController script



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        originalGravityScale = rb.gravityScale; // Store the original gravity scale
        grabbingController = GetComponentInChildren<GrabbingController>(); // Get the GrabbingController script
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button3)) && canJump)
        {
            Jump();
        }
        /*         // Check if the character is grabbing
                if (grabbingController.isGrabbing)
                {
                    //rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                    canJump = true;
                }
                else
                { */
        // Check if the character has reached the peak of the jump
        if (rb.velocity.y <= 0)
        {
            jumpPeakReached = true;
        }

        // Double the gravity if the character has reached the peak of the jump
        if (jumpPeakReached && !canJump)
        {
            rb.gravityScale = originalGravityScale * 2;
        }
        /*      } */

    }

    private void Jump()
    {
        // Apply the jump force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        print("Jumping");
        // Reset the jump peak reached flag when the character jumps
        jumpPeakReached = false;
        // Set canJump to false when the player jumps
        //canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset the gravity when the character collides with a platform or floor
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Floor"))
        {
            rb.gravityScale = originalGravityScale;
            jumpPeakReached = false;
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Set canJump to false when the character leaves the ground
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Floor"))
        {
            canJump = false;
        }
    }
}