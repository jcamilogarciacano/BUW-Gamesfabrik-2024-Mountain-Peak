using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingController : MonoBehaviour
{
    private Rigidbody2D parentRb;
    private Vector2 originalVelocity;
    private float originalGravityScale;

    public bool isGrabbing = false; // Flag to track if the GameObject is grabbing


    // Start is called before the first frame update
    void Start()
    {
        parentRb = GetComponentInParent<Rigidbody2D>();
        originalGravityScale = parentRb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Climbable"))
        {
            originalVelocity = parentRb.velocity;
            parentRb.velocity = Vector2.zero;
            parentRb.gravityScale = 0;
            isGrabbing = true; // Set isGrabbing to true when the GameObject enters a trigger
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Climbable"))
        {
            parentRb.velocity = originalVelocity;
            parentRb.gravityScale = originalGravityScale;
            isGrabbing = false; // Set isGrabbing to false when the GameObject exits a trigger

        }
    }
}