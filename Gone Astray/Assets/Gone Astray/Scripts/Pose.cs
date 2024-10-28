using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose : MonoBehaviour
{
    public Animator animator;
    public Grab[] grabScript;
    public GrapplingGun grapplingGunScript;
    public GameObject grabIcon; 
    public GameObject aimingIcon; 
    public GameObject circularIcon;

    private string grabState = "Grab";
    private string aimState = "Aim";
    private string circularState = "Circular";

    void Start()
    {
        animator.Play(grabState);
        ActivateGrab();
        ShowIcon(grabIcon);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        
        if (IsAnyGrabActive() || grapplingGunScript.isGrappling)
        {
            Debug.Log("Cannot change pose, currently grabbing or grappling!");
            return; 
        }

    
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("isAiming", true);
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isCircular", false);
            animator.Play(aimState);
            ActivateAiming();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("isCircular", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isGrabbing", false);
            animator.Play(circularState);
            ActivateCircular();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("isGrabbing", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isCircular", false);
            animator.Play(grabState);
            ActivateGrab();
        }
    }

    void ActivateGrab()
    {
        SetGrabScriptsEnabled(true);            
        grapplingGunScript.enabled = false;     
        ShowIcon(grabIcon);
        Debug.Log("Grab mode activated.");
    }

    void ActivateAiming()
    {
        SetGrabScriptsEnabled(false);           
        grapplingGunScript.enabled = true;      
        ShowIcon(aimingIcon);
        Debug.Log("Aiming mode activated.");
    }

    void ActivateCircular()
    {
        SetGrabScriptsEnabled(false);           
        grapplingGunScript.enabled = false;     
        ShowIcon(circularIcon);
        Debug.Log("Circular mode activated.");
    }

    bool IsAnyGrabActive()
    {
        foreach (Grab grab in grabScript)
        {
            if (grab != null && grab.IsGrabbing) 
            {
                return true;
            }
        }
        return false;
    }

    void SetGrabScriptsEnabled(bool isEnabled)
    {
        foreach (Grab grab in grabScript)
        {
            if (grab != null)
            {
                grab.enabled = isEnabled; 
            }
        }
    }

    void ShowIcon(GameObject iconToShow)
    {
    
        grabIcon.SetActive(false);
        aimingIcon.SetActive(false);
        circularIcon.SetActive(false);
        iconToShow.SetActive(true);
    }
}
