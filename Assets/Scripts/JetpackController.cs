using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JetpackController : MonoBehaviour
{
    public bool isJumping = false;
    public float jumpStartTime;
    public float jumpDelay = 1f; // Adjust this value to control the delay of jump
    public float jumpStrength = 5f; // Adjust this value to control the jump strength
    public float jetpackCooldown = 2f; // Adjust this value to set the cooldown time

    public Slider fuelBar; // Reference to the fuel bar

    public float jetpackDuration = 10f; // Adjust this value to set the duration of the jetpack
    public float jetpackUsed = 0f; // Adjust this value to set the used time of the jetpack
    public bool isGrounded = false;  // Check if the player is grounded

    public float raycastDistance = 0.01f; // Adjust this value to set the distance of the raycast
    void FixedUpdate()
    { 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            if (!isJumping && jetpackCooldown <= 0f)
            {
                isJumping = true;
                jumpStartTime = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            if (isJumping && jetpackUsed < jetpackDuration)
            {
                float jumpTime = Time.time - jumpStartTime;
                float jumpForce = Mathf.Clamp01(jumpTime / jumpDelay) * jumpStrength;

                // Apply the jump force to your player's rigidbody or character controller
                // Example: GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                // Apply the jump force to the PlayerControler Rigidbody2D with a softer movement
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce * 0.5f, ForceMode2D.Force);

                jetpackUsed += Time.deltaTime;
            }
            else
            {
                //get  the movementSpeedMultiplier variable from the PlayerController script and set it to 6f
                if (jetpackUsed >= jetpackDuration && jetpackUsed < jetpackDuration + 2f)
                {
                    GetComponent<PlayerController>().movementSpeedMultiplier = 6f;
                    jetpackUsed += Time.deltaTime;
                }
                else if (jetpackUsed > jetpackDuration + 2f)
                {
                    GetComponent<PlayerController>().movementSpeedMultiplier = 5f;
                }
            }
        }
        else
        {
            GetComponent<PlayerController>().movementSpeedMultiplier = 8f;
            // reduce jetpack used by time until it reaches 0
            if (jetpackUsed > 0f && !isGrounded)
            {
                jetpackUsed -= Time.deltaTime;
            }
            else if (isGrounded == true && jetpackUsed > 0)
            {
                jetpackUsed -= Time.deltaTime*2f;
                //jetpackUsed = 0f;
            }
            else if (jetpackUsed < 0f)
            {
                jetpackUsed = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (jetpackCooldown > 0f)
        {
            jetpackCooldown -= Time.deltaTime;
        }
        else if (jetpackCooldown < 0f)
        {
            jetpackCooldown = 0f;
        }

        fuelBar.value = 1f - (jetpackUsed / jetpackDuration);
    }

    //implement this function jetpackController.GetFuelLevel();
    public float GetFuelLevel()
    {
        return 1f - (jetpackUsed / jetpackDuration);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
