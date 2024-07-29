using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapHandler : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    private bool isPlaying = false; // To track the animation state

    private float tapTimeWindow = 0.3f; // Time window for detecting double tap
    private float lastTapTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoubleTap();
    }

    void DetectDoubleTap()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime < tapTimeWindow)
            {
                ToggleAnimation();
                lastTapTime = 0; // Reset last tap time
            }
            else
            {
                lastTapTime = currentTime;
            }
        }
    }

    void ToggleAnimation()
    {
        isPlaying = !isPlaying;
        animator.SetBool("isPlaying", isPlaying);
    }
}
