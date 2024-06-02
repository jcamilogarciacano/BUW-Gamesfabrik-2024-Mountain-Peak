using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Move the player with the rope segment
            player.transform.position = transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            player = other.gameObject;
            Movement movement = player.gameObject.GetComponent<Movement>();
            movement.isHangingOnRope = true;
            movement.currentRopeSegment = this.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Movement movement = player.gameObject.GetComponent<Movement>();
            movement.isHangingOnRope = false;
            movement.currentRopeSegment = null;
            //set player gravity to originalscale
            player.GetComponent<Rigidbody2D>().gravityScale = movement.originalGravityScale;
            player = null;
        }
    }
}
