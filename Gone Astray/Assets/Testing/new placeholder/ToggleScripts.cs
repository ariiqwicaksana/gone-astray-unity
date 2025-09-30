using UnityEngine;

public class ToggleScripts : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    // Animation state conditions
    private bool isGrab = true; // Default animation is Grab
    private bool isAim = false;
    private bool isCircular = false;

    void Update()
    {
        // Handle middle mouse button toggle (Grab <-> Aim)
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            ToggleGrabAndAim();
        }

        // Handle right mouse button for Circular animation
        if (Input.GetMouseButton(1)) // Right mouse button (hold)
        {
            PlayCircularAnimation();
        }
        else if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            ReturnToPreviousAnimation();
        }
    }

    void ToggleGrabAndAim()
    {
        // Toggle between Grab and Aim animations
        isGrab = !isGrab;
        isAim = !isAim;

        // Update Animator parameters
        animator.SetBool("isGrab", isGrab);
        animator.SetBool("isAim", isAim);

        // Ensure Circular is off
        isCircular = false;
        animator.SetBool("isCircular", isCircular);
    }

    void PlayCircularAnimation()
    {
        // Set Circular animation active
        isCircular = true;

        // Update Animator parameters
        animator.SetBool("isCircular", isCircular);
        animator.SetBool("isGrab", false);
        animator.SetBool("isAim", false);
    }

    void ReturnToPreviousAnimation()
    {
        // Stop Circular animation
        isCircular = false;
        animator.SetBool("isCircular", isCircular);

        // Return to the earlier state (Grab or Aim)
        animator.SetBool("isGrab", isGrab);
        animator.SetBool("isAim", isAim);
    }
}
