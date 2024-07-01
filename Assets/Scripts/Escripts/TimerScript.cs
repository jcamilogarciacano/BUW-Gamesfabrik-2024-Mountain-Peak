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
                    timerText.text = "YOU ESCAPED\n" + timerText.text;
                }
                isTimerRunning = false;
                remainingTime = remainingTime;
                timerText.color = Color.green;
                
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

}
