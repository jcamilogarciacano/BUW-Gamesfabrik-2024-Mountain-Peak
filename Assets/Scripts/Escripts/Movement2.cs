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
    Idle,
    RopeIddle,
    RopeJumping,
    RopeJumpingLeft,
    RunningIn,
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

    bool isGrounded = false;

    bool canJump = true;

    public PlayerState playerState = PlayerState.Idle;


    private Vector2 floorPosition;

    private Vector2 jumpPosition;

    public float floorCheckDistance = 1f;
    public float wallCheckDistance = 1f;

    public float wallCheckRayLength = 1f;

    public float ledgeCheckDistance = 1f;

    public float _moveHorizontal = 0f;
    public float slopeSpeed = 0f;
    float moveHorizontal = 0f;

    Vector3 freezePosition;

    bool freezePositionSet = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        _moveHorizontal = moveHorizontal;
        float moveVertical = Input.GetAxis("Vertical");
        // Prioritize state transitions
        if (Input.GetButtonDown("Stop"))
        {
            TransitionToState(PlayerState.Idle);
            return;
        }

        if (Input.GetButtonDown("DashLeft"))
        {
            TransitionToState(PlayerState.DashingLeft);
            return;
        }

        if (Input.GetButtonDown("DashRight"))
        {
            TransitionToState(PlayerState.DashingRight);
            return;
        }

        if (Input.GetButtonDown("Jump") && playerState != PlayerState.Jumping && playerState != PlayerState.Falling && playerState != PlayerState.Hanging && playerState != PlayerState.RopeIddle)
        {
            TransitionToState(PlayerState.Jumping);
            return;
        }
        else if (Input.GetButtonDown("Jump") && playerState == PlayerState.Hanging)
        {
            TransitionToState(PlayerState.Climbing);
            return;
        }
        else if (Input.GetButtonDown("Jump") && playerState == PlayerState.RopeIddle && moveHorizontal > 0)
        {
            TransitionToState(PlayerState.RopeJumping);
            return;
        }
        else if (Input.GetButtonDown("Jump") && playerState == PlayerState.RopeIddle && moveHorizontal < 0)
        {
            TransitionToState(PlayerState.RopeJumpingLeft);
            return;
        }
        if (Input.GetButtonDown("Climb") && playerState == PlayerState.Hanging)
        {
            TransitionToState(PlayerState.Climbing);
            return;
        }
        else if (Input.GetButtonDown("Climb") && playerState == PlayerState.RopeIddle)
        {
            // add the rope going up logic here
            //TransitionToState(PlayerState.Idle);
            return;
        }

        // Walking or Idle
        if (moveHorizontal != 0 && playerState != PlayerState.Jumping && playerState != PlayerState.Falling && playerState != PlayerState.Hanging && playerState != PlayerState.RopeIddle && playerState != PlayerState.RopeJumping && playerState != PlayerState.RopeJumpingLeft)
        {
            TransitionToState(PlayerState.Walking);
            spriteRenderer.flipX = moveHorizontal < 0;
        }
        else if (moveHorizontal == 0 && playerState == PlayerState.Walking)
        {
            TransitionToState(PlayerState.Idle);
        }

        if (playerState == PlayerState.RopeIddle)
        {
            //spriteRenderer.flipX = moveHorizontal < 0;
        }

        if (playerState == PlayerState.Falling)
        {
            CheckGround();
            CheckLedge();
            CheckRope();
        }


        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetInteger("PlayerState", (int)playerState);
        animator.SetFloat("DirectionX", moveHorizontal);
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Walking:
                rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), slopeSpeed * Input.GetAxis("Horizontal"));
                break;
            case PlayerState.Jumping:
                // Jump logic here
                break;
            case PlayerState.Climbing:
                //rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
                break;
            case PlayerState.DashingLeft:
                // Dash left logic here
                break;
            case PlayerState.DashingRight:
                // Dash right logic here
                break;
            case PlayerState.Falling:
                rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), -fallSpeed);
                break;
            case PlayerState.Hanging:
                // Hanging logic here
                rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), 0);
                CheckLedge();
                break;
            case PlayerState.RopeIddle:
                rb.velocity = new Vector2(0, 0);
                break;
            case PlayerState.RopeJumping:
                break;
            case PlayerState.RopeJumpingLeft:
                break;
            default:
                //  freezePositionSet = false;
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
            CheckLedge();
            //playerState = PlayerState.Idle;
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

    public void EndRopeJumping()
    {
        if (playerState == PlayerState.RopeJumping || playerState == PlayerState.RopeJumpingLeft)
        {
            playerState = PlayerState.Falling;
        }
    }

    void TransitionToState(PlayerState newState)
    {
        // Here you can handle any setup or cleanup when transitioning between states
        playerState = newState;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(floorPosition, new Vector3(0.5f, 0.1f, 1));
    }

    private void CheckWalls()
    {


    }
    //check ground
    private void CheckGround()
    {
        //  RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = Mathf.Infinity, int layerMask 
        //= Physics2D.AllLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity); 
        floorPosition = new Vector2(transform.position.x, transform.position.y - floorCheckDistance);
        //RaycastHit2D hit = Physics2D.Raycast(floorPosition, Vector2.down, rayLength, LayerMask.GetMask("Climbable"));
        //Debug.DrawRay(floorPosition, Vector2.down, Color.red, rayLength);
        RaycastHit2D BoxCast = Physics2D.BoxCast(floorPosition, new Vector2(0.5f, 0.1f), 0, Vector2.down, 0, LayerMask.GetMask("Climbable"));

        Vector2 wallDetectionPosition1 = new Vector2(transform.position.x, transform.position.y - wallCheckDistance / 3);
        Vector2 wallDetectionPosition2 = new Vector2(transform.position.x, transform.position.y - wallCheckDistance / 2);
        Vector2 wallDetectionPosition3 = new Vector2(transform.position.x, transform.position.y);
        Vector2 wallDetectionPosition4 = new Vector2(transform.position.x, transform.position.y + wallCheckDistance / 2);
        RaycastHit2D hit3 = Physics2D.Raycast(wallDetectionPosition1, Vector2.right * _moveHorizontal, wallCheckRayLength, LayerMask.GetMask("Climbable"));
        RaycastHit2D hit4 = Physics2D.Raycast(wallDetectionPosition2, Vector2.right * _moveHorizontal, wallCheckRayLength, LayerMask.GetMask("Climbable"));
        RaycastHit2D hit5 = Physics2D.Raycast(wallDetectionPosition3, Vector2.right * _moveHorizontal, wallCheckRayLength, LayerMask.GetMask("Climbable"));
        RaycastHit2D hit6 = Physics2D.Raycast(wallDetectionPosition4, Vector2.right * _moveHorizontal, wallCheckRayLength, LayerMask.GetMask("Climbable"));
        Debug.DrawRay(wallDetectionPosition1, Vector2.right * _moveHorizontal, Color.red, wallCheckRayLength);
        Debug.DrawRay(wallDetectionPosition2, Vector2.right * _moveHorizontal, Color.blue, wallCheckRayLength);
        Debug.DrawRay(wallDetectionPosition3, Vector2.right * _moveHorizontal, Color.green, wallCheckRayLength);
        Debug.DrawRay(wallDetectionPosition4, Vector2.right * _moveHorizontal, Color.yellow, wallCheckRayLength);

        //lets add a raycast to the direction the player is walking to check if it is walking on a slope
        #region floor / slopes
        RaycastHit2D hit1 = Physics2D.Raycast(floorPosition, Vector2.right, rayLength, LayerMask.GetMask("Climbable"));
        RaycastHit2D hit2 = Physics2D.Raycast(floorPosition, Vector2.left, rayLength, LayerMask.GetMask("Climbable"));
        Debug.DrawRay(floorPosition, Vector2.right, Color.red, rayLength);
        Debug.DrawRay(floorPosition, Vector2.left, Color.red, rayLength);

        if (hit3.collider == null && hit4.collider == null && hit5.collider == null && hit6.collider == null)
        {
            if (hit1.collider != null && hit2.collider != null)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
            }
            else if (hit1.collider != null || hit2.collider != null)
            {
                //transform player Y position up so  the player is not inside the slope
                slopeSpeed = 1f;
            }
            else
            {
                slopeSpeed = 0f;
            }
            #endregion

            #region wall 
        }
        else if (hit3.collider != null || hit4.collider != null)
        {
            if (freezePositionSet == false)
            {
                freezePosition = transform.position;
                freezePositionSet = true;
            }
            //create a new variable with move horizontal value to avoid the player getting stuck in the wall
            float newMoveHorizontal = Mathf.Clamp(_moveHorizontal, -0.3f, 0.3f);
            //save the player from getting stuck in the wall
            transform.position = freezePosition;
            //transform.position = new Vector2(transform.position.x + (newMoveHorizontal * -1f), transform.position.y);
        }
        if (hit3.collider == null || hit4.collider == null)
        {
            freezePositionSet = false;
        }
        #endregion

        if (BoxCast.collider == null)
        {
            playerState = PlayerState.Falling;
        }
        else if (BoxCast.collider != null)
        {
            //isGrounded = true;
            playerState = PlayerState.Idle;
        }
    }
    private void CheckLedge()
    {
        //create a  position for the ledge raycast a little bit above the player
        Vector2 ledgePosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        RaycastHit2D hit2 = Physics2D.Raycast(ledgePosition, Vector2.up, rayLength, LayerMask.GetMask("Ledge"));
        Debug.DrawRay(ledgePosition, Vector2.up, Color.blue, rayLength);

        //shoot two more raycasts to the left and right top (diagonaly) of the player to check if there is a ledge
        RaycastHit2D hit3 = Physics2D.Raycast(ledgePosition, new Vector2(-1, 1), rayLength, LayerMask.GetMask("Ledge"));
        Debug.DrawRay(ledgePosition, new Vector2(-1, 1), Color.blue, rayLength);
        RaycastHit2D hit4 = Physics2D.Raycast(ledgePosition, new Vector2(1, 1), rayLength, LayerMask.GetMask("Ledge"));
        Debug.DrawRay(ledgePosition, new Vector2(1, 1), Color.blue, rayLength);

        if (hit2.collider != null || hit3.collider != null || hit4.collider != null)
        {
            playerState = PlayerState.Hanging;
        }
        else if (hit2.collider == null && hit3.collider == null && hit4.collider == null && playerState != PlayerState.Idle && playerState != PlayerState.RopeJumping && playerState != PlayerState.RopeJumpingLeft)
        {
            playerState = PlayerState.Falling;
        }
    }

    private void CheckRope()
    {
        Vector2 ropePosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        RaycastHit2D hit5 = Physics2D.Raycast(ropePosition, Vector2.up, rayLength, LayerMask.GetMask("Rope"));
        Debug.DrawRay(ropePosition, Vector2.up, Color.green, rayLength);
        if (hit5.collider != null)
        {
            playerState = PlayerState.RopeIddle;
        }
    }
}

