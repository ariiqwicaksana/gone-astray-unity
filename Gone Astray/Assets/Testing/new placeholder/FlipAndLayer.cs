using UnityEngine;

public class FlipAndLayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the Player's position

    // SpriteRenderer references for individual parts
    public SpriteRenderer head;
    public SpriteRenderer torso;
    public SpriteRenderer pelvis;

    // Arrays for SpriteRenderers
    public SpriteRenderer[] rightArmParts; // Upper Arm Right, Lower Arm Right, Right Hand
    public SpriteRenderer[] leftArmParts;  // Upper Arm Left, Lower Arm Left, Left Hand
    public SpriteRenderer[] rightLegParts; // Upper Leg Right, Lower Leg Right, Right Feet
    public SpriteRenderer[] leftLegParts;  // Upper Leg Left, Lower Leg Left, Left Feet

    private bool isFlipped = false; // Tracks whether the player is "flipped" based on mouse position
    private bool overrideFlip = false; // Tracks whether flipping is overridden (e.g., by holding right mouse button)

    void Update()
    {
        // Check if the right mouse button is held down
        bool isRightMousePressed = Input.GetMouseButton(1); // Right mouse button

        if (isRightMousePressed != overrideFlip)
        {
            // Only update if the override state changes (pressed or released)
            overrideFlip = isRightMousePressed;

            if (overrideFlip)
            {
                // When holding the right mouse button, apply the "override" state
                OverrideFlipAndLayer();
            }
            else
            {
                // When releasing the right mouse button, revert to default behavior
                UpdateDefaultFlipAndLayer(forceUpdate: true); // Force update when returning to default
            }
        }

        // If not overriding, update the default flip logic based on mouse position
        if (!overrideFlip)
        {
            UpdateDefaultFlipAndLayer();
        }
    }

    void UpdateDefaultFlipAndLayer(bool forceUpdate = false)
    {
        // Get mouse position relative to the Player
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool shouldFlip = mousePosition.x < playerTransform.position.x;

        // Only update if the flip state changes or forceUpdate is true
        if (shouldFlip != isFlipped || forceUpdate)
        {
            UpdateFlipAndLayer(shouldFlip);
            isFlipped = shouldFlip;
        }
    }

    void UpdateFlipAndLayer(bool flip)
    {
        // Flip torso, pelvis, head, and legs horizontally
        head.flipX = flip;
        torso.flipX = flip;
        pelvis.flipX = flip;

        foreach (var part in rightLegParts)
        {
            part.flipX = flip;
        }

        foreach (var part in leftLegParts)
        {
            part.flipX = flip;
        }

        // Update flip state for arms (only flipY)
        foreach (var part in rightArmParts)
        {
            part.flipY = flip; // Flip vertically when on the left
            part.sortingOrder = flip ? -2 : 2; // Adjust order based on flip
        }

        foreach (var part in leftArmParts)
        {
            part.flipY = flip; // Flip vertically when on the left
            part.sortingOrder = flip ? 2 : -2; // Adjust order based on flip
        }

        // Update sorting order for legs
        foreach (var part in rightLegParts)
        {
            part.sortingOrder = flip ? -1 : 1;
        }

        foreach (var part in leftLegParts)
        {
            part.sortingOrder = flip ? 1 : -1;
        }
    }

    void OverrideFlipAndLayer()
    {
        // Reset flip states (no flipping when right mouse button is held)
        head.flipX = false;
        torso.flipX = false;
        pelvis.flipX = false;

        foreach (var part in rightLegParts)
        {
            part.flipX = false;
        }

        foreach (var part in leftLegParts)
        {
            part.flipX = false;
        }

        foreach (var part in rightArmParts)
        {
            part.flipY = false; // No vertical flip
            part.sortingOrder = 2; // Override sorting order for arms
        }

        foreach (var part in leftArmParts)
        {
            part.flipY = false; // No vertical flip
            part.sortingOrder = 2; // Override sorting order for arms
        }

        foreach (var part in rightLegParts)
        {
            part.sortingOrder = 1; // Override sorting order for legs
        }

        foreach (var part in leftLegParts)
        {
            part.sortingOrder = 1; // Override sorting order for legs
        }
    }
}
