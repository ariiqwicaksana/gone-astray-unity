using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyAnim : MonoBehaviour
{
    public Transform target;  // Reference to the source part
    public float force = 500;
    private float targetRot;
    private Rigidbody2D rb2d;
    private bool isDismembered = false; // To check if part is dismembered

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!isDismembered)  // Only copy rotation if not dismembered
        {
            targetRot = target.eulerAngles.z;
            rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, targetRot, force * Time.fixedDeltaTime));
        }
    }

    // Call this method when the body part is dismembered
    public void Dismember()
    {
        isDismembered = true;
    }
}
