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

    void Start()
    {
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

            // Play the typing sound
            if (typingSound != null && audioSource != null)
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
        if (audioSource != null)
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
