using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class InitialTextSpawn : MonoBehaviour
{
    //get a video player component
    public GameObject videoPlayer;
    public GameObject text;
    public GameObject plane;

    public bool useVideoPlayer = false;

    void Start()
    {
        if (useVideoPlayer)
        {

            // Construct the path to the video file
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "IntroVid.webm");

            // If running on WebGL, use UnityWebRequest to load the video
#if UNITY_WEBGL
            /* StartCoroutine(LoadVideo(videoPath)); */
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().url = videoPath;
            // Prepare the video
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
#else
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().url = videoPath;
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
            //videoPlayer.Play();
#endif
        }
    }

    bool hasPaused = false;

    void Update()
    {
        if (useVideoPlayer)
        {
            //when the video in the video player is 3 seconds in, pause the video and fade in the text
            if (videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().time >= 3.5f && !hasPaused)
            {
                videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
                //ShowText();
                FadeText();
                hasPaused = true;
            }


            if (Input.GetButtonDown("Camera"))
            {
                //continue video playback from where it left off
                videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            }
            //if the video has ended, hide the videoplane
            if (videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().time >= 6)
            {
                //plane.SetActive(false);
            }
        }
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

    // Load a video file using UnityWebRequest
    // Coroutine to load video for WebGL
    /*  private IEnumerator LoadVideo(string url)
     {
         using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestMultimedia.GetMovieTexture(url))
         {
             yield return request.SendWebRequest();

             if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
             {
                 Debug.LogError($"Error loading video: {request.error}");
                 yield break;
             }

             // Get the video texture from the request
             VideoClip videoClip = DownloadHandlerVideoClip.GetContent(request);
             videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().clip = videoClip;
         }
     } */
}
