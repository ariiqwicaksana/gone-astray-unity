using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        // Reset movement
        movement = Vector2.zero;

        // Check specific WASD key inputs
        if (Input.GetKey(KeyCode.W)) // Move up
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S)) // Move down
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) // Move left
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) // Move right
        {
            movement.x = 1;
        }
    }

    // FixedUpdate is called at a fixed interval, best for physics-based movement
    void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
