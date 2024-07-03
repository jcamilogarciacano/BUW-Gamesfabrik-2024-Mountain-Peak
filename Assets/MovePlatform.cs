using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public enum MovementDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public MovementDirection movementDirection; // Variable to hold the current movement direction
    public float moveDistance; // Distance to move
    public float moveSpeed; // Speed of movement
    public float waitTime; // Time to wait between movements

    public float moveThreshold = 0.1f; // Threshold to consider the platform at the destination
    private Vector3 initialPosition;
    private bool movingOutward = true; // Tracks whether the platform is moving out from the initial position or back to it

    public bool considerInitialPos; // Whether to consider the initial position as the starting point
    private float timer;

    private SpiderController2 spiderController2;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            spiderController2 = GetComponent<SpiderController2>();
        }
        catch (System.Exception)
        {
            spiderController2 = null;
        }
        initialPosition = transform.position;
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Vector3 moveVector = Vector3.zero;
            Vector3 targetPosition = initialPosition; // Initialize targetPosition with initialPosition
    
            // Determine the target position based on movementDirection and moveDistance
            switch (movementDirection)
            {
                case MovementDirection.Left:
                    moveVector = movingOutward ? Vector3.left : Vector3.right;
                    targetPosition += movingOutward ? Vector3.left * moveDistance : Vector3.right * moveDistance;
                    break;
                case MovementDirection.Right:
                    moveVector = movingOutward ? Vector3.right : Vector3.left;
                    targetPosition += movingOutward ? Vector3.right * moveDistance : Vector3.left * moveDistance;
                    break;
                case MovementDirection.Up:
                    moveVector = movingOutward ? Vector3.up : Vector3.down;
                    targetPosition += movingOutward ? Vector3.up * moveDistance : Vector3.down * moveDistance;
                    break;
                case MovementDirection.Down:
                    moveVector = movingOutward ? Vector3.down : Vector3.up;
                    targetPosition += movingOutward ? Vector3.down * moveDistance : Vector3.up * moveDistance;
                    break;
            }
    
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
            spiderController2?.animator.SetBool("isRotating", true);
    
            // Check distance to target position when moving outward, else check distance to initial position
            if (movingOutward && Vector3.Distance(transform.position, targetPosition) < moveThreshold)
            {
                movingOutward = !movingOutward; // Now moving inward
                print("Reached target position, moving back to initial position");
                timer = waitTime; // Reset the timer
            }
            else if (!movingOutward && Vector3.Distance(transform.position, initialPosition) < moveThreshold)
            {
                movingOutward = true; // Now moving outward
                print("Reached initial position, moving to target position");
                timer = waitTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // move the player with the platform if he is standing/colliding on top of it.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerHeight = collision.transform.position.y;
            float platformHeight = transform.position.y;
            
            // if playerstate is equals to climbing unparent the player from the platform
            if (collision.gameObject.GetComponent<Movement3>().playerState == PlayerState.Climbing)
            {
                collision.transform.SetParent(null);
            }
            // if player state is running or walking unparent the player from the platform
            else if (collision.gameObject.GetComponent<Movement3>().playerState == PlayerState.Running || collision.gameObject.GetComponent<Movement3>().playerState == PlayerState.Walking)
            {
                collision.transform.SetParent(null);
            }

            else if (playerHeight > platformHeight)
            {
                collision.transform.SetParent(transform);
            }   
        }
    }

    // remove the player from the platform if he is no longer standing/colliding on top of it.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}