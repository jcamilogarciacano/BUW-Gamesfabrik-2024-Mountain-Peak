using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // You can unlock the cursor and make it visible with some condition if needed
        // For example, unlock the cursor when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
