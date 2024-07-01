using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    public Button[] menuButtons; // Array to hold the buttons
    private int currentIndex = 0; // Index to track the currently selected button

    void Start()
    {
        // Set the first button as selected
        EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
        HighlightButton(menuButtons[currentIndex]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuButtons[currentIndex].onClick.Invoke(); // Invoke the onClick event of the selected button
        }
    }

    void MoveSelection(int direction)
    {
        // Remove highlight from the current button
        UnhighlightButton(menuButtons[currentIndex]);

        // Update the current index
        currentIndex += direction;
        if (currentIndex < 0) currentIndex = menuButtons.Length - 1;
        if (currentIndex >= menuButtons.Length) currentIndex = 0;

        // Highlight the new button
        HighlightButton(menuButtons[currentIndex]);
        EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
    }

    void HighlightButton(Button button)
    {
        // Add highlight effect (e.g., change color, scale, etc.)
        ColorBlock cb = button.colors;
        cb.normalColor = cb.highlightedColor; // Change to your highlight color
        button.colors = cb;
    }

    void UnhighlightButton(Button button)
    {
        // Remove highlight effect
        ColorBlock cb = button.colors;
        cb.normalColor = cb.pressedColor; // Change to your normal color
        button.colors = cb;
    }
}
