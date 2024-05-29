using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    public float climbSpeed = 5.0f; // The speed at which the player climbs
    public float climbDistance = 1.0f; // The distance the player can climb
    public float climbDuration = 1.0f; // The duration of the climb
    public float climbCooldown = 1.0f; // The cooldown between climbs
    public float movementSpeed = 5f; // Speed of horizontal movement
    public float jumpForce = 5f; // Force of the jump
    public float jumpAngle = 45f; // Angle of the jump in degrees
    public LayerMask climbableLayer; // Layer for climbable objects

     private bool jumpPeakReached = false; // Flag to track if the player has reached the peak of the jump
    private float originalGravityScale; // Store the original gravity scale

    public bool isClimbing = false; // Flag to track if the player is climbing
    private float climbTimer = 0.0f; // Timer for the climb duration
    private float cooldownTimer = 0.0f; // Timer for the climb cooldown
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale; // Store the original gravity scale

    }

    private void FixedUpdate()
    {
        // Get horizontal input
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Apply horizontal movement
        transform.parent.position += new Vector3(moveHorizontal * movementSpeed * Time.deltaTime, 0.0f, 0.0f);
/* 
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if (!isClimbing && cooldownTimer <= 0.0f)
            {
                isClimbing = true;
                cooldownTimer = climbCooldown;
            }
        } */

        /* if (isClimbing)
        {
            Climb();
        }

        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        // Check if the player has reached the peak of the jump
        if (rb.velocity.y <= 0 && !isClimbing)
        {
            jumpPeakReached = true;
        }
        // Double the gravity if the player has reached the peak of the jump
        if (jumpPeakReached)
        {
            rb.gravityScale = originalGravityScale * 2;
        } */
    }
    private void Jump()
    {
        if (isClimbing && !jumpPeakReached && rb.velocity.y <= 0 )
        {
           /*  // Convert the jump angle to radians
            float angle = jumpAngle * Mathf.Deg2Rad;

            // Calculate the jump vector based on the player's direction
            Vector2 jumpVector;
            if (rb.velocity.x > 0)
            {
                jumpVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * jumpForce;
            }
            else if (rb.velocity.x < 0)
            {
                jumpVector = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle)) * jumpForce;
            }
            else
            {
                jumpVector = new Vector2(0, Mathf.Sin(angle)) * jumpForce;
            }

            // Apply the jump force
            rb.AddForce(jumpVector, ForceMode2D.Force);

            // Stop climbing
            isClimbing = false; */

        }
        else
        {
            // Jump upwards without angle
           // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        // Reset the jump peak reached flag when the player jumps
        //jumpPeakReached = false;
    }
    private void Climb()
    {
      /*   if (climbTimer < climbDuration)
        {
            transform.parent.position += new Vector3(0.0f, climbSpeed * Time.deltaTime, 0.0f);
            climbTimer += Time.deltaTime;
        }
        else
        {
            isClimbing = false;
            climbTimer = 0.0f;
        } */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {/* 
        if (((1 << collision.gameObject.layer) & climbableLayer) != 0)
        {
            // If the player collides with a climbable object, make them hang onto it
            //isClimbing = true;
            rb.gravityScale = 0; // Stop falling
            rb.velocity = Vector2.zero; // Stop any existing movement
            //collision.isTrigger = true; // Make the climbing object's collider a trigger
            //rb.gravityScale = 0;
            jumpPeakReached = false;
        } */
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /* if (((1 << collision.gameObject.layer) & climbableLayer) != 0)
        {
            // If the player is still colliding with a climbable object, allow them to climb
            if (isClimbing)
            {
                rb.gravityScale = 0; // Stop falling
                rb.velocity = Vector2.zero; // Stop any existing movement
                //Climb();
            }
        } */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /* if (((1 << collision.gameObject.layer) & climbableLayer) != 0)
        {
            // If the player stops colliding with a climbable object, make them fall
            //isClimbing = false;
            rb.gravityScale = 1; // Start falling
            //collision.isTrigger = false; // Make the climbing object's collider solid
        } */
    }

}