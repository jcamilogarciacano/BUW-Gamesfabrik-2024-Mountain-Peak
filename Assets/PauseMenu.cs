using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // if press start on xbox controller, pause the game and enable the pause menu
    public GameObject pauseMenu;
    public bool isPaused = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player presses the start button on the xbox controller, pause the game
        if (Input.GetButtonDown("Start"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
        player.GetComponent<Movement3>().enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        player.GetComponent<Movement3>().enabled = true;
    }
}
