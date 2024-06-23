using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player GameObject
    public Vector3 offset; // Offset from the player's position

        public Vector3 offsetZoomedIn; // Offset from the player's position
    public float delay = 0.1f; // Delay in following the player
    public float bounceAmount = 0.2f; // Amount of bounce effect

    public bool useCameraFollow = true; // Boolean to switch between camera follow and camera movement

    public Transform[] cameraPositions; // Array of positions for camera movement
    public float cameraMoveSpeed = 2f; // Speed of camera movement

    private Vector3 velocity = Vector3.zero;
    private int currentPositionIndex = 0; // Current position index for camera movement


    // Update is called once per frame
    void FixedUpdate()
    {
        if (useCameraFollow)
        {
            CameraFollow();
        }
        else
        {
            CameraMoveBetweenPositions();
        }
    }

    // Function to handle camera follow
    private void CameraFollow()
    {
        // Calculate the desired position of the camera
        Vector3 targetPosition;
        CamZoomController camZoomController = GetComponent<CamZoomController>();
        if (camZoomController != null && camZoomController.IsZoomedIn())
        {
            targetPosition = new Vector3(transform.position.x, target.position.y + offsetZoomedIn.y, target.position.z - offsetZoomedIn.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z - offset.z);
        }

        // Apply the bounce effect
        targetPosition += CalculateBounceEffect();

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, delay);
    }

    // Function to handle camera movement between positions
    private void CameraMoveBetweenPositions()
    {
        // Check if the current position index is within the array bounds
        if (currentPositionIndex >= 0 && currentPositionIndex < cameraPositions.Length)
        {
            // Calculate the desired position of the camera
            Vector3 targetPosition = cameraPositions[currentPositionIndex].position;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);

            // Check if the camera has reached the desired position
            if (transform.position == targetPosition)
            {
                // Increment the current position index
                currentPositionIndex++;
                if (currentPositionIndex >= cameraPositions.Length)
                {
                    currentPositionIndex = 0;
                }
            }
        }
    }

    // Calculate the bounce effect based on the player's movement
    private Vector3 CalculateBounceEffect()
    {
        Vector3 bounceEffect = Vector3.zero;

        // Get the player's movement direction
        Vector3 playerMovement = target.position - transform.position;

        // Calculate the bounce effect based on the player's movement
        if (playerMovement.magnitude > 0.1f)
        {
            bounceEffect = playerMovement.normalized * bounceAmount;
        }

        return bounceEffect;
    }

    // Function to handle trigger events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NextCamera"))
        {

            // Switch to the next camera position in the array
            currentPositionIndex++;
            if (currentPositionIndex >= cameraPositions.Length)
            {
                currentPositionIndex = 0;
            }
        }
    }
}
