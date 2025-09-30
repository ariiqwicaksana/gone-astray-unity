using UnityEngine;

public class Arms : MonoBehaviour
{
    public int speed = 300;
    public Rigidbody2D rb;
    public Camera camer;

    void Update()
    {
        // Calculate the direction to the mouse position
        Vector2 direction = camer.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Convert the direction to an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate towards the target angle
        float smoothedAngle = Mathf.LerpAngle(rb.rotation, angle, speed * Time.deltaTime);

        // Apply the rotation to the Rigidbody2D
        rb.MoveRotation(smoothedAngle);
    }
}
