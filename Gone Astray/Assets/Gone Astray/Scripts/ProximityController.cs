using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityController : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup
    public RectTransform buttonRectTransform; // Reference to the button's RectTransform

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the CanvasGroup alpha is initially set to 0
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            SetButtonPosition(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            SetButtonPosition(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    private void SetButtonPosition(Transform target)
    {
        if (buttonRectTransform != null)
        {
            // Convert the target's world position to screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
            // Set the button's position to the screen position
            buttonRectTransform.position = screenPos;
        }
    }
}

