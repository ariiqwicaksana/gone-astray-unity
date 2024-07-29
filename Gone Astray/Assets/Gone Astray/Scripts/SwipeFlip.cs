using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeFlip : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // Record start position of the touch
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                // Record end position of the touch and process the swipe
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        float swipeThreshold = 50f; // Minimum distance for a swipe to be registered
        float swipeDistance = (endTouchPosition - startTouchPosition).magnitude;

        if (swipeDistance > swipeThreshold)
        {
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Horizontal swipe
                if (swipeDirection.x > 0)
                {
                    // Right swipe
                    FlipSprite(false);
                }
                else
                {
                    // Left swipe
                    FlipSprite(true);
                }
            }
        }
    }

    private void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
}
