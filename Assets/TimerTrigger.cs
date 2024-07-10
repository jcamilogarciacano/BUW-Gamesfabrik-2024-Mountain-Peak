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
        //set the timer text gameobjcet to false
        //TimerScript.Instance.TimerText.gameObject.SetActive(false);
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
                        //set the timerText text mesh pro  to true
                        TimerScript.Instance.TimerText.enabled = true;
                //start cororutine
                StartCoroutine(StartTimer());
            }
            else if(timeStopper)
            {
                TimerScript.Instance.EndReached = true;
            }
        }
    }

    //corroutine to start timer after 5 seconds
    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        TimerScript.Instance.IsTimerRunning = true;
    }
}
