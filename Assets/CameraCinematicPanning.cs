using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematicPanning : MonoBehaviour
{

    // sprite renderer fade color from black to white
    public SpriteRenderer fadeSprite;
    //create a boolean to tell when the camera should start panning
    public bool startPanning = false;

    //create a target position for the camera to pan to
    public Vector3 targetPosition;

    //create a time for the camera to pan to the target position
    public float time;

    private float elapsedTime = 0f; // Track the elapsed time since panning started
    private bool isPanning = false; // Track if we are currently in the process of panning

    public bool startPanning2 = false;

    //variable to gameplay camera object
    public GameObject gameplayCamera;

    //variable to player object
    public GameObject player;

    public GameObject playerIntro;

    // Assuming these are class members
    float lerpFactor = 0f; // Initialize to 0
    float lerpSpeed = 0.01f; // Control how fast lerpFactor increases, adjust as needed
    public InitialTextSpawn initialTextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        //set the color of the fade sprite to black
        fadeSprite.color = new Color(0.3f, 0.3f, 0.3f, 1);
        initialTextSpawn = GameObject.Find("InitialPlayer").GetComponent<InitialTextSpawn>();
    }

    void Update()
    {
        //get input "Camera"
        if (Input.GetButtonDown("Camera"))
        {
            startPanning = true;
            SpriteRenderer spriteRenderer = initialTextSpawn.text.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            //StartCoroutine(initialTextSpawn.FadeSpriteAlphaToZero(2f, spriteRenderer));
        }

        if (startPanning && !isPanning)
        {
            // Reset elapsed time and mark as panning
            elapsedTime = 0f;
            isPanning = true;
            startPanning = false; // Prevent re-entry
            Invoke("PanToTarget", 2f);
            playerIntro.GetComponent<Animator>().SetTrigger("StartIntro");
        }

        if (isPanning)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the progress ratio
            float progress = elapsedTime / time;
            if (progress > 1f)
            {
                progress = 1f;
                isPanning = false; // Stop panning once the target time is reached
            }

            //call pan to target with an invoke delay


            if (startPanning2)
            {
                lerpFactor += Time.deltaTime * lerpSpeed;
                lerpFactor = Mathf.Clamp(lerpFactor, 0, 1);

                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, lerpFactor);

                //check if the position of the camera is close to the target position
                if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f || lerpFactor >= 1)
                {
                    startPanning2 = false;
                    print("Camera reached target position");
                    Invoke("DisableCamera", 3f);
                    lerpFactor = 0; // Reset lerpFactor for the next panning operation
                }
            }
            // Update fade color
            fadeSprite.color = Color.Lerp(new Color(0.3f, 0.3f, 0.3f, 1), new Color(1, 1, 1, 1), progress);
        }
    }

    //funcion for the camera to pan to the target position
    public void PanToTarget()
    {
        startPanning2 = true;
    }

    //create a function to disable this camera, and enable the gameplay camera and player
    public void DisableCamera()
    {
        //disable this camera
        gameObject.GetComponent<Camera>().enabled = false;
        //enable the gameplay camera
        gameplayCamera.SetActive(true);
        //enable the player
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<Movement3>().enabled = true;
        print("Camera disabled");
    }
}
