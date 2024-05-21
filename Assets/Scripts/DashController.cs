using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashDuration = 2f;
    public float cooldownTime = 0.3f;
    private Rigidbody2D rb;
    private float cooldownTimer = 0f;
    private bool isDashingLeft = false;
    private bool isDashingRight = false;
    private float dashTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Check if cooldown timer has expired
        if (cooldownTimer <= 0f)
        {
            // Start dashing
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButton("LeftTrigger"))
            {
                isDashingLeft = true;
                dashTimer = dashDuration;
                cooldownTimer = cooldownTime;
            }
            // Start dashing
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("RightTrigger"))
            {
                isDashingRight = true;
                dashTimer = dashDuration;
                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            // Reduce cooldown timer
            cooldownTimer -= Time.deltaTime;
        }

        // Apply dash force while dashing
        if (isDashingLeft || isDashingRight)
        {
            if (dashTimer > 0f)
            {
                if (isDashingLeft)
                    rb.AddForce(Vector2.left * dashForce, ForceMode2D.Force);
                else if (isDashingRight)
                    rb.AddForce(Vector2.right * dashForce, ForceMode2D.Force);
                dashTimer -= Time.deltaTime;
            }
            else
            {
                isDashingLeft = false;
                isDashingRight = false;
            }
        }
    }
}
