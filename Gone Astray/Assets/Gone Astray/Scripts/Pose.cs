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
    public GameObject NormalIcon;

    public LineRenderer aimLineRenderer; // Add a reference to the aim line

    private string grabState = "Grab";
    private string aimState = "Aim";
    private string circularState = "Circular";
    private string normalState = "Normal";

    void Start()
    {
        animator.Play(normalState);
        ActivateNormal();
        ShowIcon(NormalIcon);
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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetBool("isAiming", true);
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isCircular", false);
            animator.SetBool("isNormal", false);
            animator.Play(aimState);
            ActivateAiming();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animator.SetBool("isCircular", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isNormal", false);
            animator.Play(circularState);
            ActivateCircular();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("isGrabbing", true);
            animator.SetBool("isAiming", false);
            animator.SetBool("isCircular", false);
            animator.SetBool("isNormal", false);
            animator.Play(grabState);
            ActivateGrab();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("isNormal", true);
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isCircular", false);
            animator.SetBool("isAiming", false);
            animator.Play(normalState);
            ActivateNormal();
        }
    }

    void ActivateGrab()
    {
        SetGrabScriptsEnabled(true);
        grapplingGunScript.enabled = false;
        aimLineRenderer.enabled = false; // Disable aim line in Grab mode
        ShowIcon(grabIcon);
        Debug.Log("Grab mode activated.");
    }

    void ActivateAiming()
    {
        SetGrabScriptsEnabled(false);
        grapplingGunScript.enabled = true;
        aimLineRenderer.enabled = true; // Enable aim line in Aiming mode
        ShowIcon(aimingIcon);
        Debug.Log("Aiming mode activated.");
    }

    void ActivateCircular()
    {
        SetGrabScriptsEnabled(false);
        grapplingGunScript.enabled = false;
        aimLineRenderer.enabled = false; // Disable aim line in Circular mode
        ShowIcon(circularIcon);
        Debug.Log("Circular mode activated.");
    }

    void ActivateNormal()
    {
        SetGrabScriptsEnabled(false);
        grapplingGunScript.enabled = false;
        aimLineRenderer.enabled = false; // Disable aim line in Normal mode
        ShowIcon(NormalIcon);
        Debug.Log("Normal mode activated.");
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
        NormalIcon.SetActive(false);
        iconToShow.SetActive(true);
    }
}
