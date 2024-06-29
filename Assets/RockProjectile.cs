using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    //create a variable for how much it adds push force
    public float pushForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // on collider2d enter with a gameobject with tag player, push the player the direction of the rock
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Rock hit player");
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                collision.gameObject.GetComponent<Movement3>().SetRockHitted();
                playerRigidbody.AddForce((transform.position.x < collision.transform.position.x ? Vector2.left : Vector2.right) * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
