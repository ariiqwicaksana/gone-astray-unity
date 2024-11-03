using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMechanic : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject grabbedObject; 
    public float grabRange = 1.5f; 
    public float throwForce = 10f;
    public bool IsGrabbing => isGrabbing; // Maximum range to detect objects for grabbing // Kekuatan lemparan

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrabbing)
            {
                ThrowObject();
            }
            else
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
                    rb.bodyType = RigidbodyType2D.Kinematic; // Set menjadi Kinematic saat diambil
                    grabbedObject.transform.position = transform.position; // Tempatkan objek di posisi pet
                    grabbedObject.transform.SetParent(transform); // Set sebagai anak
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
                rb.bodyType = RigidbodyType2D.Dynamic; // Kembalikan ke Dynamic saat dilempar
                grabbedObject.transform.SetParent(null); // Lepaskan objek dari pet
                Vector2 throwDirection = transform.right; // Arah lemparan
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse); // Tambahkan gaya lempar
                grabbedObject = null; 
                isGrabbing = false; 
            }
        }
    }
}
