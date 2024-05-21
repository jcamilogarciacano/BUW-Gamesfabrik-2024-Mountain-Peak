using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomController : MonoBehaviour
{
    public bool zoomedIn = false; // Boolean state to control zoom

    public float zoomSpeed = 5f; // Speed of the camera zoom
    public float zoomedInSize = 5f; // Orthographic size when zoomed in
    public float zoomedOutSize = 10f; // Orthographic size when zoomed out

    private Camera cam;

    private float targetSize;
    private float currentSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        currentSize = cam.orthographicSize;
        targetSize = zoomedOutSize;
    }

    private void FixedUpdate()
    {
        // Check for state change and update target size accordingly
        if (zoomedIn)
        {
            targetSize = zoomedInSize;
        }
        else
        {
            targetSize = zoomedOutSize;
        }

        // Smoothly interpolate the camera size towards the target size
        currentSize = Mathf.Lerp(currentSize, targetSize, zoomSpeed * Time.deltaTime);
        cam.orthographicSize = currentSize;
    }
}
