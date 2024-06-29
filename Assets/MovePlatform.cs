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

    private Vector3 initialPosition;
    private bool movingOutward = true; // Tracks whether the platform is moving out from the initial position or back to it

    public bool considerInitialPos; // Whether to consider the initial position as the starting point
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Vector3 moveVector = Vector3.zero;
            switch (movementDirection)
            {
                case MovementDirection.Left:
                    moveVector = movingOutward ? Vector3.left : Vector3.right;
                    break;
                case MovementDirection.Right:
                    moveVector = movingOutward ? Vector3.right : Vector3.left;
                    break;
                case MovementDirection.Up:
                    moveVector = movingOutward ? Vector3.up : Vector3.down;
                    break;
                case MovementDirection.Down:
                    moveVector = movingOutward ? Vector3.down : Vector3.up;
                    break;
            }

            transform.Translate(moveVector * moveSpeed * Time.deltaTime);

            if (Mathf.Abs(Vector3.Distance(transform.position, initialPosition)) >= moveDistance)
            {
                // Only reverse direction if moving outward and the platform has reached the moveDistance
                movingOutward = !movingOutward; // Now moving inward
                timer = waitTime; // Reset the timer
            }
            else if (considerInitialPos && Vector3.Distance(transform.position, initialPosition) < 0.1f && !movingOutward)
            {
                // Only reverse direction if considering initial position, close to initial position, and currently moving inward
                movingOutward = true; // Now moving outward
                timer = waitTime;
            }
            else if (!considerInitialPos)
            {
                // If not considering initial position, always reset the timer to 0 to keep moving
                timer = 0;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}