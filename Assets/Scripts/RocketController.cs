using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float maxForce = 1000f; // Maximum force to be applied
    public float forceIncreaseRate = 100f; // Rate at which the force increases
    public float disableForceTime = 2f; // Maximum time to disable the force after it ends
    private float currentForce = 0f; // Current force being applied
    private bool isForceActive = false; // Flag to track if the force is active
    private float forceEndTime = 0f; // Time when the force ends

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isForceActive)
            {
                // Cancel the force
                isForceActive = false;
                currentForce = 0f;
                forceEndTime = Time.time + disableForceTime;
            }
            else
            {
                // Start applying the force
                isForceActive = true;
                forceEndTime = 0f;
            }
        }

        if (isForceActive)
        {
            // Increase the force gradually
            currentForce += forceIncreaseRate * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0f, maxForce);

            // Apply the force upwards
            rb.AddForce(Vector2.up * currentForce);
        }
        else if (forceEndTime > 0f && Time.time >= forceEndTime)
        {
            // Disable the force after the specified time
            forceEndTime = 0f;
        }
    }
}
