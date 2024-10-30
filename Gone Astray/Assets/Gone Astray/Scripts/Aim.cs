using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform startPoint;  // Assign this in the Inspector
    public float lineLength = 10f;  // Length of the line
    public LayerMask collisionLayer; // Set to layer 9 in the Inspector or programmatically

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Set the line to only have two positions
        lineRenderer.positionCount = 2;

        // Define the layer mask if not set in the Inspector
        collisionLayer = LayerMask.GetMask("Grappable"); // Replace with your layer's name
    }

    void Update()
    {
        // Calculate the direction based on the world rotation of the start point
        Vector3 direction = startPoint.right.normalized;  // Adjust if needed

        // Calculate the end position based on the line length and direction
        Vector3 endPosition = startPoint.position + direction * lineLength;

        // Draw a debug ray to visualize the path (optional)
        Debug.DrawRay(startPoint.position, direction * lineLength, Color.red);

        // Perform a raycast only on layer 9 (using collisionLayer)
        RaycastHit2D hit = Physics2D.Raycast(startPoint.position, direction, lineLength, collisionLayer);

        // Check if the raycast hit something
        if (hit.collider != null)
        {
            // If it hits, set the line end point to the collision point
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If it doesn't hit anything, set the line end point to the calculated endPosition
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}
