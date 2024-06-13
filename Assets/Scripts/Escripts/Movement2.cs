using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walking,
    Jumping,
    DashingLeft,
    DashingRight,
    Climbing,
    Hanging,
    Falling,
    Idle
}

public class Movement2 : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    public float fallSpeed = 10f;
    public float dashSpeed = 20f;
    public float climbingSpeed = 0.1f;
    public float rayLength = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public PlayerState playerState = PlayerState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
            playerState = PlayerState.Walking;
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
            playerState = PlayerState.Walking;
        }

        if (IsGrounded() && Mathf.Abs(moveHorizontal) < 0.1f && playerState != PlayerState.Jumping && playerState != PlayerState.Climbing && playerState != PlayerState.Falling)
            playerState = PlayerState.Idle;

        if (Input.GetButtonDown("Jump") && playerState != PlayerState.Jumping)
        {
            playerState = PlayerState.Jumping;
        }

        if (Input.GetButtonDown("Climb"))
        {
            playerState = PlayerState.Climbing;
        }

        if (Input.GetButtonDown("DashLeft"))
        {
            playerState = PlayerState.DashingLeft;
        }

        if (Input.GetButtonDown("DashRight"))
        {
            playerState = PlayerState.DashingRight;
        }

        if (Input.GetButtonDown("Stop"))
        {
            playerState = PlayerState.Idle;
        }

        // Check if the player is not on the ground and is not already jumping or climbing
        if (!IsGrounded() && playerState != PlayerState.Jumping && playerState != PlayerState.Climbing)
        {
            playerState = PlayerState.Falling;
        }
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetInteger("PlayerState", (int)playerState);
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Walking:
                rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
                break;
            case PlayerState.Jumping:
                //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                break;
            case PlayerState.Climbing:
                rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
                break;
            case PlayerState.DashingLeft:
                //rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                break;
            case PlayerState.DashingRight:
                //rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                break;
            case PlayerState.Falling:
                rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
                break;
            default:
                rb.velocity = new Vector2(0, 0);
                break;
        }
    }

    public void EndJumping()
    {
        if (playerState == PlayerState.Jumping)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void EndClimbing()
    {
        if (playerState == PlayerState.Climbing)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void EndDashingLeft()
    {
        if (playerState == PlayerState.DashingLeft)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void EndDashingRight()
    {
        if (playerState == PlayerState.DashingRight)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void EndFalling()
    {
        if (playerState == PlayerState.Falling)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void EndWalking()
    {
        if (playerState == PlayerState.Walking)
        {
            playerState = PlayerState.Idle;
        }
    }
    bool IsGrounded()
    {
        float extraHeightText = 1f;
        
        RaycastHit2D raycastHit = Physics2D.Raycast(rb.position, Vector2.down, extraHeightText, LayerMask.GetMask("Climbable"));
        Debug.DrawRay(rb.position, Vector2.down, Color.blue, extraHeightText);
        if (raycastHit.collider != null)
        {
            print("IsGrounded with " + raycastHit.collider.name);
            return true;
        }
        else
        {
            print("Not Grounded");
            return false;
        }
        
    }
}