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
        // Get the horizontal movement
        // float moveHorizontal = Input.GetAxis("Horizontal");

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
            isDashing = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFalling = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isClimbing = false;
            isDashing = false;
            isFalling = false;
            isJumping = false;
        }
        // Dash to the left
        //animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsDashing", isDashing);
        animator.SetBool("IsHanging", isHanging);
        animator.SetBool("IsClimbing", isClimbing);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsHangingOnRope", isHangingOnRope);
    }

}