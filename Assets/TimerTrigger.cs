using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public bool timeStarter = false;
    public bool timeStopper = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //on trigger2d enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //start timer
            if(timeStarter)
            {
                TimerScript.Instance.IsTimerRunning = true;
            }
            else if(timeStopper)
            {
                TimerScript.Instance.EndReached = true;
            }
        }
    }
}
