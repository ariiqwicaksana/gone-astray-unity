using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Transform[] logos; 
    public Transform Pet;

    private void Update() {
    
        if (Input.GetKey(KeyCode.Q)) {
            RotateObject(rotationSpeed);
        }

        if (Input.GetKey(KeyCode.E)) {
            RotateObject(-rotationSpeed);
        }
    }

    private void RotateObject(float speed)
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
        foreach (Transform logo in logos)
        {
            if (logo != null)
            {
                logo.Rotate(0, 0, speed * Time.deltaTime);
            }
        }
    }
    private void RotatePet(float speed)
    {
        if (Pet != null)
        {
            Pet.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
