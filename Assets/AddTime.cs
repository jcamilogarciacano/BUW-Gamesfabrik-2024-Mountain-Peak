using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //on trigger2d enter get timer script isntance and add time
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // add a random time between 60 and 120 seconds
            TimerScript.Instance.RemainingTime += Random.Range(60, 120);
            Destroy(gameObject);
        }
    }
}
