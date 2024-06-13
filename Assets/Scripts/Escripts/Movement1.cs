using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    public float swingForce = 5f;
    public float dashSpeed = 20f;

    public float climbingSpeed = 0.1f;
    public float originalGravityScale;
    public bool isJumping = false;
    public bool isDashingLeft = false;

    public bool isDashingRight = false;
    public bool isDashing = false;

    // Add a new boolean variable to track whether the player is hanging
    public bool isHanging = false;
    public bool isClimbing = false;
    public bool isFalling = false;
    public bool isHangingOnRope = false;

    public bool isWalking = false;

    private Rigidbody2D rb;
    private Animator animator;

    private float increaseYSpeed = 0.1f; // Adjust this value as needed
    private Vector3 targetPosition;
    private bool shouldMove = false;
    public bool platformOnTop = false;
    public bool platformBellow = false;

    // add a variable to store the collisioned object
    private GameObject collidedObject;

    public GameObject currentRopeSegment;

    public SpriteRenderer spriteRenderer;

    public float rayLength = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get the horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Flip the player's orientation based on the direction of movement if should move is false


        // Flip the player's orientation based on the direction of movement if should move is false
        if (moveHorizontal > 0)
        {
            // Flip the sprite on the x-axis
            spriteRenderer.flipX = false;
            // move the character to the right, it has a kinematic rigidbody.
            transform.position += Vector3.right * speed * Time.deltaTime;
            if(!isDashingLeft && !isDashingRight && !isJumping && !isClimbing && !isHanging && !isHangingOnRope){
                isWalking = true;
            }
            //isWalking = true;
        }
        else if (moveHorizontal < 0)
        {
            // Flip the sprite on the x-axis
            spriteRenderer.flipX = true;
            // move the character to the left , it has a kinematic rigidbody.
            transform.position += Vector3.left * speed * Time.deltaTime;
            if(!isDashingLeft && !isDashingRight && !isJumping && !isClimbing && !isHanging && !isHangingOnRope){
                isWalking = true;
            }
            //isWalking = true;
        }

        if (Mathf.Abs(moveHorizontal) < 0.1f)
            isWalking = false;

        // if space is jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
        // if player opresses W is climbing
        if (Input.GetKeyDown(KeyCode.W))
        {
            isClimbing = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isDashingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDashingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isClimbing = false;
            isDashing = false;
            isFalling = false;
            isJumping = false;
        }

        // shoot a raycast upwards if is jumping is true, and if it collides with a platform, then stop the rb from jumping
        if (isJumping)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, rayLength, LayerMask.GetMask("Ledge"));
            Debug.DrawRay(transform.position, Vector2.up, Color.blue, rayLength);
            if (hit2.collider != null && hit2.collider.tag == "Platform")
            {
                print("Player is jumping and hit a platform");
                isJumping = false;
                isHanging = true;
            }
        }
        //freeze the player Y transform if is hanging is true
        if (isHanging)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, rayLength, LayerMask.GetMask("Ledge"));
            Debug.DrawRay(transform.position, Vector2.up, Color.blue, rayLength);
           //if the collider is false and also not colliding with a platform, then the player is falling
            if (hit2.collider == null || hit2.collider.tag != "Platform")
            {
                isHanging = false;
                isFalling = true;
            }

        }
        //  lets detect by using a raycast downwards if the player is standing on a platform
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, LayerMask.GetMask("Climbable"));

        //RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.up, rayLength, LayerMask.GetMask("Climbable"));
        //show the raycast in the editor
        Debug.DrawRay(transform.position, Vector2.down, Color.red, rayLength);
        if (hit.collider != null && hit.collider.tag == "Platform")
        {
            print("Player is standing on a platform");
            platformBellow = true;
            //stop the rb from falling
            if(!isJumping && !isClimbing && !isDashingLeft && !isDashingRight && !isHanging && !isHangingOnRope)
                rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            platformBellow = false;
            //isFalling = true;
            //if falling is true, lets simulate gravity the rigidbody is kinematic so we need to simulate gravity
            if(!isJumping && !isClimbing && !isDashingLeft && !isDashingRight && !isHanging && !isHangingOnRope)
                rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
        }


        /*         if (hit.collider != null && hit.collider.tag == "Platform")
                {
                    print("Player is standing on a platform");
                    platformBellow = true;
                }
                else
                {
                    platformBellow = false;
                    // if the player is not standing on a platform, lets check if the player is hanging on a platform
                    RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, 1f);
                    if (hit2.collider != null && hit2.collider.tag == "Platform")
                    {
                        platformOnTop = true;
                    }
                    else
                    {
                        platformOnTop = false;
                        // if the player is not either standing on a platform or hanging on a platform, lets check if the player is hanging on a rope
                        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.right, 1f);
                        if (hit3.collider != null && hit3.collider.tag == "Rope")
                        {
                            isHangingOnRope = true;
                            currentRopeSegment = hit3.collider.gameObject;
                        }
                        else
                        {
                            isHangingOnRope = false;
                            //finally if the player is not standing on a platform, hanging on a platform or hanging on a rope, lets check if the player is hanging on a ledge
                            RaycastHit2D hit4 = Physics2D.Raycast(transform.position, Vector2.left, 1f);
                            if (hit4.collider != null && hit4.collider.tag == "Platform")
                            {
                                isHanging = true;
                                collidedObject = hit4.collider.gameObject;
                            }
                            else
                            {
                                isHanging = false;
                                // if it is not hanging on a ledge, lets check if the player is falling
                                if (!isJumping && !isClimbing && !isDashingLeft && !isDashingRight && !isHanging && !isHangingOnRope)
                                {
                                    isFalling = true;
                                    //if falling is true, lets simulate gravity the rigidbody is kinematic so we need to simulate gravity
                                    rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
                                }
                            }
                        }

                    }
                }
         */

        // Dash to the left
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsDashingLeft", isDashingLeft);
        animator.SetBool("IsDashingRight", isDashingRight);
        animator.SetBool("IsHanging", isHanging);
        animator.SetBool("IsClimbing", isClimbing);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsHangingOnRope", isHangingOnRope);
        animator.SetBool("IsWalking", isWalking);
    }

    // create methods to call them rom the animation events for turning true or false the isDashingLeft parameter on animator 
    public void StartDashingLeft()
    {
        isDashingLeft = true;
    }
    //end dashing
    public void EndDashingLeft()
    {
        isDashingLeft = false;
    }
    //end dashing right
    public void EndDashingRight()
    {
        isDashingRight = false;
    }
    //start walking
    public void StartWalking()
    {
        isWalking = true;
    }
    //end walking
    public void EndWalking()
    {
        isWalking = false;
    }
    //end climbing
    public void EndClimbing()
    {
        isClimbing = false;
    }
    // end jumping
    public void EndJumping()
    {
        isJumping = false;
    }
    //start falling
    public void StartFalling()
    {
        isFalling = true;
    }
    //end falling
    public void EndFalling()
    {
        isFalling = false;
    }
    // lets simulate graviy when the player is falling, we will use the FixedUpdate method
    // the rigidbody is kinematic so we need to simulate gravity
    void FixedUpdate()
    {
        if (isJumping)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //isJumping = false;
        }
        if (isClimbing)
        {
            //rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
        }
        if (isDashingLeft)
        {
            //rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        }
        if (isDashingRight)
        {
            //rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        if (isFalling)
        {
            //rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
        }
    }

}