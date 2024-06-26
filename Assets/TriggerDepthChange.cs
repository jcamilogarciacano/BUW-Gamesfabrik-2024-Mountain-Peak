using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDepthChange : MonoBehaviour
{
    //create two bools one for in and other for back
    public bool inTrigger = false;
    public bool backTrigger = false;

    //on trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the object that enters the trigger is the player
        if (other.gameObject.CompareTag("Player"))
        {
            //if in trigger, run function on player 
            if (inTrigger)
            {
                other.gameObject.GetComponent<Movement3>().EndRunningIn();
            }
            //if back trigger, run function on player
            else if (backTrigger)
            {
                other.gameObject.GetComponent<Movement3>().EndRunningBack();
            }
        }
    }
}
