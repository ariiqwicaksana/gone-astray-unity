using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMechanic : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject grabbedObject; 
    public float grabRange = 1.5f; 
    public float throwForce = 10f;
    public bool IsGrabbing => isGrabbing; 
    public Pet pet; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrabbing)
            {
                ThrowObject();
            }
            else if (pet.canGrab) 
            {
                TryGrabObject();
            }
        }
    }

    void TryGrabObject()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, grabRange);
        
        foreach (Collider2D col in objectsInRange)
        {
            if (col.CompareTag("Grabbable"))
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

                if (rb != null && grabbedObject == null)
                {
                    grabbedObject = col.gameObject;
                    rb.bodyType = RigidbodyType2D.Kinematic; 
                    grabbedObject.transform.position = transform.position; 
                    grabbedObject.transform.SetParent(transform); 
                    isGrabbing = true; 
                    break;
                }
            }
        }
    }

    void ThrowObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; 
                grabbedObject.transform.SetParent(null); 
                Vector2 throwDirection = transform.right; 
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse); 
                grabbedObject = null; 
                isGrabbing = false; 
            }
        }
    }
}
