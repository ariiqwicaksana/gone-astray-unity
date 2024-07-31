using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    public Transform grabPoint; // Point where the object will be held
    private GameObject grabbedObject = null; // Currently grabbed object
    private bool isGrabbing = false; // Flag to track grabbing state

    // Method to toggle grabbing
    public void ToggleGrab()
    {
        isGrabbing = !isGrabbing;

        if (isGrabbing && grabbedObject != null)
        {
            // Grab the object
            grabbedObject.transform.SetParent(grabPoint);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true; // Optional: make the object kinematic to prevent physics interactions while grabbed
        }
        else if (grabbedObject != null)
        {
            // Release the object
            grabbedObject.transform.SetParent(null);
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false; // Optional: revert the object to non-kinematic
            grabbedObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grabbable") && !isGrabbing)
        {
            grabbedObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == grabbedObject && !isGrabbing)
        {
            grabbedObject = null;
        }
    }
}
