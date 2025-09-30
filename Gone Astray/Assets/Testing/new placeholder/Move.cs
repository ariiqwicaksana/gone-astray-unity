using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float touchForce = 6f;
    public Rigidbody2D rb2d;
    public grab hand1, hand2;

    private Vector2 movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reset movement
        movement = Vector2.zero;

        // Input handling for movement
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }

        // Normalize movement vector to prevent faster diagonal movement
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Adjust touchForce based on rope holding
        if (hand1.holdingRope || hand2.holdingRope)
        {
            touchForce = 23f; // Increased force when holding a rope
        }
        else
        {
            touchForce = 6f; // Normal force
        }
    }

    void FixedUpdate()
    {
        // Move the character using Rigidbody2D
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Apply force for horizontal movement (adds a sliding physics effect)
        if (movement.x != 0)
        {
            Vector2 force = Vector2.right * touchForce * movement.x;
            rb2d.AddForce(force);
        }
    }
}
