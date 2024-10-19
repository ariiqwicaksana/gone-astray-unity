using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    
    // Animation state names in the Animator
    private string grabState = "Grab";
    private string aimState = "Aim";
    private string circularState = "Circular";

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, you can set the default state (grab) at the start
        animator.Play(grabState);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Press 2 to switch to "Aim" animation
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.Play(aimState);
            animator.SetBool("isAiming", true);
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isCircular", false);
        }

        // Press C to switch to "Circular" animation
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.Play(circularState);
            animator.SetBool("isCircular", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isGrabbing", false);
        }

        // Press 1 to switch back to "Grab" animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.Play(grabState);
            animator.SetBool("isGrabbing", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isCircular", false);
        }
    }
}
