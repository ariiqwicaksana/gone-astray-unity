using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyAnim : MonoBehaviour
{
    public Transform target;  // Reference to the source part
    private Rigidbody2D rb2d;
    private bool isDismembered = false; // To check if part is dismembered
    
    public float forceNormal = 500f;
    public float forceGrab = 700f;
    public float forceAiming = 300f;
    public float forceCircular = 100f;
    private float currentForce;

    private enum ForceType
    {
        Normal,
        Grab,
        Aiming,
        Circular
    }

    private ForceType currentForceType = ForceType.Normal;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetForce();
    }

    void FixedUpdate()
    {
        if (!isDismembered)  // Only copy rotation if not dismembered
        {
            float targetRot = target.eulerAngles.z;
            rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, targetRot, currentForce * Time.fixedDeltaTime));
        }
        
        CheckForceInput();
    }

    void CheckForceInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetForceType(ForceType.Normal);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SetForceType(ForceType.Grab);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SetForceType(ForceType.Aiming);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SetForceType(ForceType.Circular);
    }

    void SetForceType(ForceType newForceType)
    {
        currentForceType = newForceType;
        SetForce();
    }

    void SetForce()
    {
        switch (currentForceType)
        {
            case ForceType.Normal:
                currentForce = forceNormal;
                break;
            case ForceType.Grab:
                currentForce = forceGrab;
                break;
            case ForceType.Aiming:
                currentForce = forceAiming;
                break;
            case ForceType.Circular:
                currentForce = forceCircular;
                break;
        }
    }

    // Call this method when the body part is dismembered
    public void Dismember()
    {
        isDismembered = true;
    }
}
