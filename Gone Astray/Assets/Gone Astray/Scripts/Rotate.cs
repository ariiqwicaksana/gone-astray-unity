using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    private void Update() {
        // Check if Q or E keys are pressed for rotation
        if (Input.GetKey(KeyCode.Q)) {
            // Rotate counterclockwise
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E)) {
            // Rotate clockwise
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
