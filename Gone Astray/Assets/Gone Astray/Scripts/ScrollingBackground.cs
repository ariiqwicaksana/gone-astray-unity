using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform playerTransform; 
    public float parallaxFactor = 0.5f;

    private Vector3 lastPlayerPosition; 

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        Vector3 deltaPosition = playerTransform.position - lastPlayerPosition;
        transform.position += new Vector3(deltaPosition.x * parallaxFactor, deltaPosition.y * parallaxFactor, 0);
        lastPlayerPosition = playerTransform.position;
    }
}
