using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     // add on collision enter 2d to detect if player collides with gameobject with tag "Jetpack"
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JetPack"))
        {
            // get the JetpackController script and set the jetpackCooldown to 2f
            JetpackController jetpackController = GetComponent<JetpackController>();
            jetpackController.enabled = true;
            Destroy(collision.gameObject);
        }
    }
}
