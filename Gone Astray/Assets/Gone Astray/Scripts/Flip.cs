using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {
    void Update() {
        // Check if the right arrow key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Rotate to face forward (0 degrees on the Y-axis)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Check if the left arrow key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Rotate to face backward (180 degrees on the Y-axis)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
