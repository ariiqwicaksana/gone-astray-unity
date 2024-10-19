using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCam; // Reference to the Cinemachine virtual camera
    public float zoomSpeed = 2f;                   // How fast to zoom in and out
    public float minZoom = 3f;                     // Minimum orthographic size (zoom in limit)
    public float maxZoom = 10f;                    // Maximum orthographic size (zoom out limit)

    void Update()
    {
        // Get the current orthographic size from the Cinemachine camera
        float currentZoom = cinemachineCam.m_Lens.OrthographicSize;

        // Check if the UpArrow or DownArrow keys are pressed and adjust zoom accordingly
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentZoom -= zoomSpeed * Time.deltaTime;  // Zoom in when UpArrow is pressed
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentZoom += zoomSpeed * Time.deltaTime;  // Zoom out when DownArrow is pressed
        }

        // Clamp the zoom to be between the min and max values
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Apply the zoom value back to the Cinemachine camera
        cinemachineCam.m_Lens.OrthographicSize = currentZoom;
    }
}

