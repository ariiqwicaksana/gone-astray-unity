using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool isGrabbing = false; // Flag to track whether the player is grabbing an object
    private FixedJoint2D currentJoint; // To store the currently active joint
    public float grabRange = 1.5f; // Maximum range to detect objects for grabbing

    // Update is called once per frame
    void Update()
    {
        // Toggle grab/release when F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrabbing)
            {
                // If already grabbing, release the object
                ReleaseObject();
            }
            else
            {
                // Try to grab an object if not already grabbing
                TryGrabObject();
            }
        }
    }

    // Method to try grabbing an object within a specified range
    void TryGrabObject()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, grabRange);
        
        foreach (Collider2D col in objectsInRange)
        {
            if (col.CompareTag("Grabbable")) // Only grab objects with the tag "Grabbable"
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

                if (rb != null && currentJoint == null) // Check that no joint is currently active
                {
                    // Create a FixedJoint2D and connect to the object
                    currentJoint = gameObject.AddComponent<FixedJoint2D>();
                    currentJoint.connectedBody = rb;
                    isGrabbing = true; // Set grabbing to true
                    break; // Stop after grabbing one object
                }
            }
        }
    }

    // Method to release the grabbed object
    void ReleaseObject()
    {
        if (currentJoint != null) // Ensure a joint exists before trying to destroy it
        {
            Destroy(currentJoint); // Remove the joint
            currentJoint = null; // Clear the reference
            isGrabbing = false; // Set grabbing to false
        }
    }

    // Optional: Visualize the grab range in the Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}

