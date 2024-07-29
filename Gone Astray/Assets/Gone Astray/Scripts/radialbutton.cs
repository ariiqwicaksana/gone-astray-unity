using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radialbutton : MonoBehaviour
{
    [SerializeField] RectTransform handle; // Reference to the RectTransform of the button
    [SerializeField] Transform character; // Reference to the Transform of the character
    [SerializeField] CanvasGroup canvasGroup; // Reference to the CanvasGroup of the Circle (Image)
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0; // Set the initial alpha to 0 (transparent)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when the handle is being dragged
    public void OnDrag()
    {
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;

        // Apply the rotation to the handle
        Quaternion r = Quaternion.AngleAxis(angle, Vector3.forward);
        handle.rotation = r;

        // Apply the same rotation to the character
        character.rotation = Quaternion.Euler(0, 0, angle);
    }

    // This method is called when dragging starts
    public void OnBeginDrag()
    {
        canvasGroup.alpha = 1; // Set the alpha to 1 (non-transparent) when dragging starts
    }

    // This method is called when dragging ends
    public void OnEndDrag()
    {
        canvasGroup.alpha = 0; // Set the alpha back to 0 (transparent) when dragging ends
    }
}
