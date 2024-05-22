using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private float moveDirection;
    private Animator animator;
    private bool isJumping;
    private float jumpStartTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's movement input
        moveDirection = Input.GetAxis("Horizontal");

        // Flip the sprite based on the movement direction
        if (moveDirection < 0)
        {
            // Moving left, flip the sprite
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            // Moving right, restore the sprite's original orientation
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Check if the player presses the space key to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpStartTime = Time.time;
        }

        // Check if the player releases the space key to stop jumping
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Set the "Jump" trigger parameter to play the jumping animation
        animator.SetBool("Jump", isJumping && GetComponent<PlayerController>().isGrounded);

        // Adjust the speed of the jumping animation clip based on how long the player holds the space key
        if (isJumping)
        {
            float jumpDuration = 1f - (Time.time - jumpStartTime) / 2f;
            animator.SetFloat("JumpSpeed", jumpDuration);
        }
        
    }
}
