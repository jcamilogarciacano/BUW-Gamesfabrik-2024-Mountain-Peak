using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    public float swingForce = 5f;
    public float dashSpeed = 20f;

    public float climbingSpeed = 0.1f;
    public float originalGravityScale;
    public bool isJumping = false;
    public bool isDashing = false;
    // Add a new boolean variable to track whether the player is hanging
    public bool isHanging = false;
    public bool isClimbing = false;
    public bool isFalling = false;
    public bool isHangingOnRope = false;

    private Rigidbody2D rb;
    private Animator animator;

    private float increaseYSpeed = 0.1f; // Adjust this value as needed
    private Vector3 targetPosition;
    private bool shouldMove = false;
    public bool platformOnTop = false;

    // add a variable to store the collisioned object
    private GameObject collidedObject;

    public GameObject currentRopeSegment;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Get the horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (isHanging)
        {
            moveHorizontal *= 0.5f;
        }
        if(isHangingOnRope)
        {
            
            moveHorizontal = 0;
        }
        // Move the player horizontally
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // Flip the player's orientation based on the direction of movement if should move is false
        if (moveHorizontal > 0 && !shouldMove)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveHorizontal < 0 && !shouldMove)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // Jump if the player presses the jump button and is not already jumping
        if (Input.GetButtonDown("Jump") && !isJumping && !isHanging && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
        // Dash to the left
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            StartCoroutine(Dash(-dashSpeed));
        }

        // Dash to the right
        if (Input.GetKeyDown(KeyCode.E) && !isDashing)
        {
            StartCoroutine(Dash(dashSpeed));
        }


        // Double the gravity if the player is falling
        if (isJumping && rb.velocity.y < 0 && !isHangingOnRope)
        {
            rb.gravityScale = originalGravityScale * 3;
        }

        if (rb.velocity.y < -0.1f && !isHanging && !isClimbing)
        {
            isFalling = true;
            //InvokeRepeating("RemoveConstraint", 2f, 0.2f);
            if (!isHanging && !isClimbing && !isJumping && !isDashing && !isFalling)
            {
                // InvokeRepeating("RemoveConstraint", 1f, 0.2f);
            }
        }
        else
        {
            isFalling = false;
        }

/*         if (isHangingOnRope)
        {
              //set gravity to 0
                rb.gravityScale = 0;
            // If the player presses A, apply a force to the left
            if (Input.GetKey(KeyCode.A))
            {   
                rb.AddForce(new Vector2(-swingForce, 0));
            }
            // If the player presses D, apply a force to the right
            else if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector2(swingForce, 0));
            }
            // If the player presses Space, apply an upward force and stop hanging
            if (Input.GetKey(KeyCode.Space))
            {
                rb.gravityScale = originalGravityScale;
                rb.AddForce(new Vector2(0, jumpForce));
                isHangingOnRope = false;
                currentRopeSegment = null;
            }
            else if (currentRopeSegment != null)
            {
                // Move the current rope segment with the player
               // currentRopeSegment.transform.position = transform.position;
            }
        } */
        // If the player is not hanging or climbing or jumping or anything, after 2 seconds, reset the y constraint
        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsDashing", isDashing);
        animator.SetBool("IsHanging", isHanging);
        animator.SetBool("IsClimbing", isClimbing);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsHangingOnRope", isHangingOnRope);
    }

    void FixedUpdate()
    {
        if (shouldMove)
        {
            //animator.speed = 0; // Pause the animation
            Climb();
        }
        else
        {
            animator.speed = 1; // Resume the animation
        }
    }
    // Coroutine for dashing
    IEnumerator Dash(float dashSpeed)
    {
        isDashing = true;
        for (int i = 0; i < 10; i++)
        {
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
            yield return new WaitForSeconds(0.1f); // Apply force every 0.1 second
        }
        isDashing = false;
    }
    void Climb()
    {
        if (isClimbing)
        {
            return;
        }
        isHanging = true;

        // If the player presses W, move the player up // remove this part from 
        if (Input.GetKey(KeyCode.W))
        {
            // Resume the animation
            isHanging = false;

            if (!platformOnTop)
            {
                // Calculate the direction to move on the x-axis based on the relative position of the platform
                float moveX = Mathf.Sign(collidedObject.transform.position.x - transform.position.x);

                // Update the target position to move up and a little on the x-axis
                targetPosition = new Vector3(transform.position.x + moveX, collidedObject.transform.position.y + 0.9f, transform.position.z);

                // Transform the position until the target position is reached
                InvokeRepeating("UpdatePosition", 0f, climbingSpeed);
                //InvokeRepeating("RemoveConstraint", 2f, 0.2f);
            }
            else
            {
                // Update the target position to move up
                targetPosition = new Vector3(transform.position.x, collidedObject.transform.position.y + 0.9f, transform.position.z);

                // Transform the position until the target position is reached
                InvokeRepeating("UpdatePosition", 0f, climbingSpeed);
                //InvokeRepeating("RemoveConstraint", 2f, 0.2f);
            }
        }
        // if player presses S A or D should move false and disable all constraints
        else if (Input.GetKey(KeyCode.S))
        {
            isHanging = false;
            /*  shouldMove = false;
             rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionY; // Remove FreezePositionY from the current constraints
  */
            print("S pressed");
            //update the target position to collided object
            //targetPosition = new Vector3(transform.position.x, collidedObject.transform.position.y - (transform.position.y - 0.9f), transform.position.z);
            targetPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            //transform the position unitl satisfy the if condition
            // Use InvokeRepeating to update the position every 0.1 seconds
            InvokeRepeating("UpdatePositionDown", 0f, 0.1f);
        }
    }

    // create a small fuction to remove the constraint on Y after some seconds
    void RemoveConstraint()
    {
        rb.constraints = rb.constraints & ~RigidbodyConstraints2D.FreezePositionY; // Remove FreezePositionY from the current constraints
        CancelInvoke("RemoveConstraint");
    }
    void UpdatePosition()
    {
        isClimbing = true;
        //animator.SetLayerWeight(1, 1); // Enable the climbing animation layer
        // Move towards the target position
/*         if(Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f){
            
        } */
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, increaseYSpeed);
        //freeze x and y movement 
        rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        // Check if the target position is reached
        //compare this floating values with a tolerance
        if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f)
        {
            shouldMove = false;
            // Disable all constraints
            rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionY; // Remove FreezePositionY from the current constraints
            //disable the freeze position x too
            rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionX; // Remove FreezePositionX from the current constraints
            // Stop invoking the UpdatePosition method
            platformOnTop = false;
            isClimbing = false;
            //animator.SetLayerWeight(1, 0); // Disable the climbing animation layer
            CancelInvoke("UpdatePosition");
        }
    }
    //create me a function like update position but for the down key
    void UpdatePositionDown()
    {
        //add a new position just below the player
        //targetPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, increaseYSpeed);
        //freeze x and y movement
        rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        // Check if the target position is reached
        //compare this floating values with a tolerance
        if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f)
        {
            shouldMove = false;
            // Disable all constraints
            rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionY; // Remove FreezePositionY from the current constraints
                                                                                      //disable the freeze position x too
            rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionX; // Remove FreezePositionX from the current constraints
                                                                                      // Stop invoking the UpdatePosition method
            platformOnTop = false;
            CancelInvoke("UpdatePositionDown");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset the gravity and jumping state when the player hits the ground
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Floor"))
        {
            print("Collided with platform");
            collidedObject = collision.gameObject;
            rb.gravityScale = originalGravityScale;
            isJumping = false;
            isDashing = false;

            // Cast a ray upwards from the player's position let me adjust the lentgh of the ray
            Vector2 raycastPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
            // Create a variable for the length of the ray
            float rayLength = 5f;
            // Create a raycast hit variable to store the hit information
            // let the raycast2d hit ignore the player layer so it only collides with Platform layer objects
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.up, rayLength, LayerMask.GetMask("Climbable"));
            //show the raycast in the editor
            Debug.DrawRay(raycastPosition, Vector2.up, Color.red, rayLength);

            // If the ray hits the platform and the platform is higher than the player by 0.5
            if (hit.collider == collision.collider && collision.transform.position.y > transform.position.y)
            {
                platformOnTop = true;
                // freeze the player's y position
                rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY; // Add FreezePositionY to the current constraints
                //also gravity 0
                //rb.gravityScale = 0;

                // Set the target position and enable movement
                targetPosition = new Vector3(transform.position.x, collision.transform.position.y + 0.9f, transform.position.z);
                shouldMove = true;
                print("Hit the same platform");
            }
            // If the ray does not hit the same platform but the player collided with a platform higher in Y than him
            else if (hit.collider != collision.collider && collision.transform.position.y > transform.position.y)
            {
                rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY; // Add FreezePositionY to the current constraints
                // Set the target position and enable movement
                targetPosition = new Vector3(transform.position.x, collision.transform.position.y + 0.9f, transform.position.z);
                shouldMove = true;
                print("Hit a different platform");
            }
        }
    }

    // on collision exit shoul move false
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Floor"))
        {
            if (collision.transform.position.y > transform.position.y)
            {
                isHanging = false;
                shouldMove = false;
                rb.constraints = rb.constraints & ~RigidbodyConstraints2D.FreezePositionY; // Remove FreezePositionY from the current constraints
            }
        }
        // this should only be false only if the player leaves collision while being underneath the collided platform
    }
}