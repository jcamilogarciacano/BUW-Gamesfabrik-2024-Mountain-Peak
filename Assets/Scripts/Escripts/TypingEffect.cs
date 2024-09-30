using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the TMP_Text component
    public float typingSpeed = 0.05f; // Speed of typing (time between characters)
    public AudioClip typingSound; // Reference to the typing sound
    private AudioSource audioSource; // Reference to the AudioSource component

    private string fullText; // The full text to display
    private string currentText = ""; // The text that is currently being displayed

    private GameObject loadingText;

    void Start()
    {
        //THE GAME OBJECT is child of Canvas/slide3/LOADING
        loadingText = GameObject.Find("Canvas/slide3/LOADING");

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        // Debug logs to check component assignments
        if (uiText == null)
        {
            Debug.LogError("uiText is not assigned in the Inspector.");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
        }

        // Get the full text from the TMP_Text component
        if (uiText != null)
        {
            fullText = uiText.text;
            // Clear the text in the TMP_Text component
            uiText.text = "";
            // Start the typing effect
            // StartCoroutine(TypeText());
            StartTyping();
        }
        

        // Get the full text from the TMP_Text component
        // fullText = uiText.text;
        // Clear the text in the TMP_Text component
        // uiText.text = "";
        // Get the AudioSource component
        // audioSource = GetComponent<AudioSource>();
        // Start the typing effect
        // StartCoroutine(TypeText());
    }
    public void StartTyping()
    {
        StopAllCoroutines();
        currentText = "";
        uiText.text = "";
        StartCoroutine(TypeText());
    }

    void Update()
    {
        // if the loading text is active, stop the typing effect
        if (loadingText.activeSelf)
        {
            StopAllCoroutines();
            audioSource.Stop();
        }
    }
    IEnumerator TypeText()
    {
        Debug.Log("Typing started for: " + gameObject.name);

        if (uiText == null)
        {
            yield break;
        }
         foreach (char c in fullText.ToCharArray())
        {
            currentText += c;
            uiText.text = currentText;

            //exit the for each if the loading text is active
            if(loadingText.activeSelf)
            {
                yield break;
            }
            // Play the typing sound only if loadint text is disabled
            if (typingSound != null && audioSource != null )
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(typingSound);
                }
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        Debug.Log("Typing finished for: " + gameObject.name);

        // Stop the typing sound when typing is complete
        if (audioSource != null && loadingText.activeSelf == true)
        {
            audioSource.Stop();
        }
    }

    // Optional: Method to trigger the typing effect manually
    // public void StartTyping(string newText)
    // {
    //     StopAllCoroutines();
    //     fullText = newText;
    //     currentText = "";
    //     if (uiText != null)
    //     {
    //         uiText.text = "";
    //         StartCoroutine(TypeText());
    //     }
    // }
}
