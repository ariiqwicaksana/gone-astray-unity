using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public float speed = 100f;
    public RectTransform image2;        // Assign Image 2's RectTransform in the Inspector
    public GameObject textObject;       // Assign the text GameObject in the Inspector
    public GameObject otherPanel;       // Assign the other panel GameObject in the Inspector
    private RectTransform rectTransform;
    private bool isOverlapping = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textObject.SetActive(false);    // Ensure the text is inactive initially
        otherPanel.SetActive(false);    // Ensure the other panel is inactive initially
    }

    void Update()
    {
        // Movement code for Image 1
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical) * speed * Time.deltaTime;
        rectTransform.anchoredPosition += movement;

        // Check for overlap with Image 2
        isOverlapping = IsOverlapping(rectTransform, image2);
        textObject.SetActive(isOverlapping);

        // Check if Enter is pressed and Image 1 is overlapping with Image 2
        if (isOverlapping && Input.GetKeyDown(KeyCode.Return))
        {
            otherPanel.SetActive(true); // Activate the other panel
        }
    }

    bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        // Calculate boundaries of the RectTransforms
        Rect rect1Bounds = new Rect(
            rect1.anchoredPosition.x - rect1.rect.width / 2,
            rect1.anchoredPosition.y - rect1.rect.height / 2,
            rect1.rect.width,
            rect1.rect.height
        );

        Rect rect2Bounds = new Rect(
            rect2.anchoredPosition.x - rect2.rect.width / 2,
            rect2.anchoredPosition.y - rect2.rect.height / 2,
            rect2.rect.width,
            rect2.rect.height
        );

        // Check if the Rects overlap
        return rect1Bounds.Overlaps(rect2Bounds);
    }
}
