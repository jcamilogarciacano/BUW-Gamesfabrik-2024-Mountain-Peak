using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortal : MonoBehaviour
{
    //animator for the portal
    
    public Animator portalAnimator;  

    // Start is called before the first frame update
    void Start()
    {
        portalAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerScript.Instance.EndReached == true)
        {
            portalAnimator.SetTrigger("ClosePortal");
        }
    }

    // Open the portal on trigger enter2d
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        //set animator parameter trigger OpenPortal
        portalAnimator.SetTrigger("OpenPortal");
        }
    }
    //on trigger exit 2d
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //set animator parameter trigger ClosePortal
            portalAnimator.SetTrigger("ClosePortal");
        }
    }
}
