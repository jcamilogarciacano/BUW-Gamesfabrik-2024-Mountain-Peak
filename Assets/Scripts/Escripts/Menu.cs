using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public Button newGameButton;
    public Button leaderboardButton;
    public Button exitButton;
    public AudioClip navigationSound;
    private AudioSource audioSource;
    private Button[] buttons;
    private int currentIndex = 0;
    private float lastInputTime = 0f; // Track the time since the last input
    private float inputCooldown = 0.5f; // Cooldown period in seconds

    void Start()
    {
        newGameButton.onClick.AddListener(() => LoadScene("TutorialScene"));
        leaderboardButton.onClick.AddListener(() => LoadScene("LeaderboardScene"));
        exitButton.onClick.AddListener(ExitGame);

        buttons = new Button[] { newGameButton, leaderboardButton, exitButton };

        // Set the first button as selected
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleKeyboardAndJoystickInput();
    }

    void HandleKeyboardAndJoystickInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        bool downPressed = Input.GetKeyDown(KeyCode.DownArrow) || (verticalInput < -0.5f && Time.time - lastInputTime > inputCooldown);
        bool upPressed = Input.GetKeyDown(KeyCode.UpArrow) || (verticalInput > 0.5f && Time.time - lastInputTime > inputCooldown);

        if (downPressed)
        {
            MoveSelection(1);
            lastInputTime = Time.time; // Update the last input time
            PlayNavigationSound();
        }
        else if (upPressed)
        {
            MoveSelection(-1);
            lastInputTime = Time.time; // Update the last input time
            PlayNavigationSound();
        }

        // Check for "A" button press or Enter/Return key for selection
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PlayNavigationSound();
            buttons[currentIndex].onClick.Invoke();
            lastInputTime = Time.time; // Update the last input time
        }
    }

    void MoveSelection(int direction)
    {
        // Update the current index
        currentIndex += direction;
        if (currentIndex < 0) currentIndex = buttons.Length - 1;
        if (currentIndex >= buttons.Length) currentIndex = 0;

        // Deselect current
        EventSystem.current.SetSelectedGameObject(null);
        // Set the new button as selected
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void PlayNavigationSound()
    {
        if (audioSource != null && navigationSound != null)
        {
            audioSource.PlayOneShot(navigationSound);
        }
    }
}
