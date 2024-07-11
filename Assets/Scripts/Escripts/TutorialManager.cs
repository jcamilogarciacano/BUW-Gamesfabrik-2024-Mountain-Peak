using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSlides; // Array to hold the slides
    public Button leftButton;
    public Button rightButton;
    public Button playButton;
    public Image[] dotIndicators; // Array to hold the dot indicators
    private float joystickCooldown = 0.5f; // 0.5 seconds cooldown
    private float lastJoystickInputTime = 0f; // Time since the last joystick input
    private int currentSlideIndex = 0;
    public AudioClip navigationSound; // Reference to the navigation sound
    public AudioClip playSound; // Reference to the play button sound
    private AudioSource audioSource; // AudioSource component to play the sound
    // private TypingEffect typingEffect; // Reference to the TypingEffect script

    void Start()
    {
        UpdateSlides();

        leftButton.onClick.AddListener(ShowPreviousSlide);
        rightButton.onClick.AddListener(ShowNextSlide);
        playButton.onClick.AddListener(LoadGameScene);

        audioSource = GetComponent<AudioSource>();
        // typingEffect = tutorialSlides[currentSlideIndex].GetComponentInChildren<TypingEffect>();

    }

    void Update()
    {
        HandleKeyboardAndJoystickInput();
    }

    void HandleKeyboardAndJoystickInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Time.time - lastJoystickInputTime > joystickCooldown)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || horizontalInput < 0)
            {
                // PlaySound(navigationSound);
                ShowPreviousSlide();
                lastJoystickInputTime = Time.time; // Reset the timer
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || horizontalInput > 0)
            {
                // PlaySound(navigationSound);
                ShowNextSlide();
                lastJoystickInputTime = Time.time; // Reset the timer
            }
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit")) && currentSlideIndex == tutorialSlides.Length - 1)
        {
            PlaySound(navigationSound);
            LoadGameScene();
        }
    }

    void ShowPreviousSlide()
    {
        if (currentSlideIndex > 0)
        {
            currentSlideIndex--;
            UpdateSlides();
        }
    }

    void ShowNextSlide()
    {
        if (currentSlideIndex < tutorialSlides.Length - 1)
        {
            currentSlideIndex++;
            UpdateSlides();
        }
        else if (currentSlideIndex == tutorialSlides.Length - 1)
        {
            LoadGameScene();
        }
    }

    void UpdateSlides()
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
        {
            tutorialSlides[i].SetActive(i == currentSlideIndex);
        }

        // Update button interactability
        leftButton.interactable = currentSlideIndex > 0;
        rightButton.interactable = currentSlideIndex < tutorialSlides.Length - 1;

        // Assuming tutorialSlides is not null and has at least one slide
        if (tutorialSlides != null && tutorialSlides.Length > 0)
        {
            // Disable the right button on the last slide
            if (currentSlideIndex == tutorialSlides.Length - 1)
            {
                rightButton.interactable = false;
                // Optional: To make the button not show, you can also adjust its visibility
                rightButton.gameObject.SetActive(false);
            }
            else
            {
                rightButton.interactable = true;
                // Optional: Ensure the button is visible if previously hidden
                rightButton.gameObject.SetActive(true);
            }
        }

        UpdateDotIndicators();
        // Trigger the typing effect for the current slide
        TMP_Text textComponent = tutorialSlides[currentSlideIndex].GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            TypingEffect typingEffect = textComponent.GetComponent<TypingEffect>();
            if (typingEffect != null)
            {
                typingEffect.StartTyping();
            }
        }
    }

    void UpdateDotIndicators()
    {
        for (int i = 0; i < dotIndicators.Length; i++)
        {
            dotIndicators[i].color = (i == currentSlideIndex) ? Color.white : Color.gray; // Change colors as needed
        }
    }

    void LoadGameScene()
    {
        //load Demo3 scene as single
        SceneManager.LoadScene("Demo3", LoadSceneMode.Single);

    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
