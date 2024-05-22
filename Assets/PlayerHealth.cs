using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    // add lives variables
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock"))
        {
            spriteRenderer.enabled = false;
            GetComponent<PlayerController>().enabled = false;
            TMP_Text textMesh = GetComponentInChildren<TMP_Text>();
            if (textMesh != null)
            {
                print("Player's Y position: " + transform.localPosition.y);
                textMesh.text = transform.localPosition.y.ToString("F0");
                gameObject.transform.rotation = Quaternion.identity;
            }

            Invoke("RestartScene", 3f);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
