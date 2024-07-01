using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDetectionHelper : MonoBehaviour
{
    //player entered the shooting zone
    public bool startShooting = false; // Whether the player entered the shooting zone

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Add an OnTrigger2DEnter method to detect when the player is inside the shooting zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startShooting = true;
        }
    }
    //ontrigger exit2d
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startShooting = false;
        }
    }
}
