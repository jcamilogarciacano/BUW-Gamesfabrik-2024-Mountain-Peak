using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform topPosition; // Reference to the top position game object
    public Transform bottomPosition; // Reference to the bottom position game object
    public float moveSpeed = 5f; // Speed at which the elevator moves

    private bool playerOnElevator = false; // Flag to check if the player is on the elevator

    private void Update()
    {
        if (playerOnElevator)
        {
            // Move the elevator towards the top position
            transform.position = Vector3.MoveTowards(transform.position, topPosition.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Move the elevator towards the bottom position
            transform.position = Vector3.MoveTowards(transform.position, bottomPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // playerOnElevator = true;
            StartCoroutine(MoveElevatorWithDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // playerOnElevator = false;
               StartCoroutine(MoveElevatorWithDelay());
        }
    }

    private IEnumerator MoveElevatorWithDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 2 seconds before moving the elevator

        playerOnElevator = !playerOnElevator;
    }
}
