using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    
    // Animation state names in the Animator
    public Grab[] grabScript; // Reference to the Grab script
    public GrapplingGun grapplingGunScript;
    public GameObject grabIcon; 
    public GameObject aimingIcon; 
    public GameObject circularIcon;
    private string grabState = "Grab";
    private string aimState = "Aim";
    private string circularState = "Circular";

    // Start is called before the first frame update
    void Start()
    {
    
        // Optionally, you can set the default state (grab) at the start
        animator.Play(grabState);
        ActivateGrab();
        ShowIcon(grabIcon);
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
            ActivateAiming();
            grabIcon.SetActive(false);
            aimingIcon.SetActive(true);
            circularIcon.SetActive(false);
        }

        // Press C to switch to "Circular" animation
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.Play(circularState);
            animator.SetBool("isCircular", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isGrabbing", false);
            ActivateCircular();
            grabIcon.SetActive(false);
            aimingIcon.SetActive(false);
            circularIcon.SetActive(true);
        }

        // Press 1 to switch back to "Grab" animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.Play(grabState);
            animator.SetBool("isGrabbing", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isCircular", false);
            ActivateGrab();
            grabIcon.SetActive(true);
            aimingIcon.SetActive(false);
            circularIcon.SetActive(false);
        }
    }
    void ActivateGrab()
    {
        foreach (Grab grab in grabScript)
        {
            if (grab != null)
            {
                grab.enabled = true; 
            }
        }
        if (grapplingGunScript != null)
        {
            grapplingGunScript.enabled = false;
        }
        Debug.Log(" Grab scripts activated, GrapplingGun deactivated");
    }

    void ActivateAiming()
    {
        foreach (Grab grab in grabScript)
        {
            if (grab != null)
            {
                grab.enabled = false; 
            }
        }
        if (grapplingGunScript != null)
        {
            grapplingGunScript.enabled = true; 
        }
        Debug.Log("Aiming activated, Grab  deactivated");
    }

    void ActivateCircular()
    {
        foreach (Grab grab in grabScript)
        {
            if (grab != null)
            {
                grab.enabled = false; 
            }
        }
        if (grapplingGunScript != null)
        {
            grapplingGunScript.enabled = false; 
        }
        Debug.Log("Circular activated, all Grab and GrapplingGun deactivated");
    }
    void ShowIcon(GameObject iconToShow)
    {
        
        grabIcon.SetActive(true);
        aimingIcon.SetActive(false);
        circularIcon.SetActive(false);

        iconToShow.SetActive(true);
    }
}
