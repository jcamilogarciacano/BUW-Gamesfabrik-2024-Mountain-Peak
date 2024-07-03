using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialTextSpawn : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowText()
    {
        text.SetActive(true);
    }

    public void FadeText()
    {
        SpriteRenderer spriteRenderer = text.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0;

        StartCoroutine(FadeSpriteAlphaToFull(5f, spriteRenderer));
    }

    public IEnumerator FadeSpriteAlphaToFull(float t, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        float alpha = spriteRenderer.color.a;
        for (float j = 0; j < 1; j += Time.deltaTime / t)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha + j);
            yield return null;
        }
    }

    //another fade method to go to alpha 0
    public IEnumerator FadeSpriteAlphaToZero(float t, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        float alpha = spriteRenderer.color.a;
        for (float j = 0; j < 1; j += Time.deltaTime / t)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha - j);
            yield return null;
        }
    }
}
