using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSlides; // Array to hold the slides
    public Button leftButton;
    public Button rightButton;
    public Button playButton;
    public Image[] dotIndicators; // Array to hold the dot indicators

    private int currentSlideIndex = 0;

    void Start()
    {
        UpdateSlides();

        leftButton.onClick.AddListener(ShowPreviousSlide);
        rightButton.onClick.AddListener(ShowNextSlide);
        playButton.onClick.AddListener(LoadGameScene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
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
    }

    void UpdateSlides()
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
        {
            tutorialSlides[i].SetActive(i == currentSlideIndex);
        }

        leftButton.interactable = currentSlideIndex > 0;
        rightButton.interactable = currentSlideIndex < tutorialSlides.Length - 1;

        UpdateDotIndicators();
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
        SceneManager.LoadScene("Demo3");
    }
}
