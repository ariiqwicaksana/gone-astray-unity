using UnityEngine;

public class balance : MonoBehaviour
{
    public float targetRot;
    public Rigidbody2D rb2d;
    public float force;

    void Update()
    {
        // Smoothly rotate the Rigidbody2D towards the target rotation
        rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, targetRot, force * Time.fixedDeltaTime));
    }
}
