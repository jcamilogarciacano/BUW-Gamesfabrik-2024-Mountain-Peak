using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public static TimerScript Instance { get; private set; } // Singleton instance
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    float startingTime;
    bool isTimerRunning = false;

    bool endReached = false;

    public GameObject player;
    public GameObject victoryPosition;

    public GameObject clouds;

    public bool EndReached
    {
        get => endReached;
        set => endReached = value;
    }
    public float RemainingTime
    {
        get => remainingTime;
        set => remainingTime = value;
    }

    public TextMeshProUGUI TimerText
    {
        get => timerText;
        set => timerText = value;
    }
    public bool IsTimerRunning
    {
        get => isTimerRunning;
        set => isTimerRunning = value;
    }
    // Update is called once per frame

    private void Awake()
    {
        // Check if instance already exists and if it's not this one, destroy it.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: Makes it persist across scene loads
        }
    }

    void Start()
    {
        startingTime = remainingTime;
        //remainingTime = 600f;
        timerText.color = Color.white;
        isTimerRunning = false;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void Update()
    {
        if (remainingTime <= startingTime * 0.25f)
        {
            // danger zone close to no time left
            timerText.color = Color.red;

        }
        if (remainingTime > 0)
        {
            if (isTimerRunning)
            {
                print("Timer is running");
                remainingTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            // add a message of "YOU ESCAPED" with the timer text
            if (endReached)
            {
                if(isTimerRunning)
                {
                    //show the timerText but instead of time left, show the time taken to escape in the format minutes:seconds
                    int minutes = Mathf.FloorToInt((startingTime - remainingTime) / 60);
                    int seconds = Mathf.FloorToInt((startingTime - remainingTime) % 60);
                    timerText.text = string.Format("CONGRATULATIONS! YOU ESCAPED IN {0:00}:{1:00}", minutes, seconds);

                }
                //find the player with Player tag 
                player = GameObject.FindGameObjectWithTag("Player");
                // find the victory position with VictoryPosition tag
                victoryPosition = GameObject.FindGameObjectWithTag("VictoryPosition");
                // find the clouds with Clouds tag
                clouds = GameObject.FindGameObjectWithTag("Clouds");
                //enable the sprite renderer on clouds
                clouds.GetComponent<SpriteRenderer>().enabled = true;
                // enable the animator component on clouds
                clouds.GetComponent<Animator>().enabled = true;
                //disable movement3 script on player
                player.GetComponent<Movement3>().enabled = false;
                //disable the player collider
                //player.GetComponent<Collider2D>().enabled = false;
                //disable the player rigidbody
                //player.GetComponent<Rigidbody2D>().simulated = false;
                //disable player sprite renderer
                player.GetComponent<SpriteRenderer>().enabled = false;
                
                player.transform.position = victoryPosition.transform.position;
                //enable the sprite renderer and animator on the gameobject child of victoryposition
                victoryPosition.GetComponentInChildren<SpriteRenderer>().enabled = true;
                victoryPosition.GetComponentInChildren<Animator>().enabled = true;
                //animate the player
                //player.GetComponent<Animator>().SetBool("VictoryState", true);
                // set the player position to the victory position
                isTimerRunning = false;
                remainingTime = remainingTime;
                // color rgb rgb(59,32,149)
                timerText.color = new Color(59, 32, 149);
                
            }
        }
        else if (remainingTime <= 0)
        {
            timerText.text = "GAME OVER";
            isTimerRunning = false;
            remainingTime = 0;
            // make the game time super slow
            Time.timeScale = 0.1f;
            // call restart scene after a delay
            Invoke("RestartScene", 1f);
        }

        // remainingTime -= Time.deltaTime;

    }

    //create a function to restart the scene
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    // function to go back to main menu scene
    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

}
