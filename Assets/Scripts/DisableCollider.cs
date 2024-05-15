using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
    public float offset = 1f; // The offset to move the collider down

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            other.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            other.isTrigger = false;
        }
    }

    private bool isDownKeyPressed = false; // Flag to track if the down key is being pressed

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            isDownKeyPressed = true;
            MoveColliderDown();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            isDownKeyPressed = false;
            ResetColliderPosition();
        }
    }

    private void MoveColliderDown()
    {
        if (isDownKeyPressed)
        {
            transform.position -= new Vector3(0f, offset, 0f);
        }
    }

    private void ResetColliderPosition()
    {
        transform.position += new Vector3(0f, offset, 0f);
    }
}
