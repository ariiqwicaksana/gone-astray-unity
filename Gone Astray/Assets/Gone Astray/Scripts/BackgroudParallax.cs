using UnityEngine;

public class BcakgroundParallax : MonoBehaviour
{
    public Transform cameraTransform; 
    public float parallaxFactor = 0.5f; 
    private Vector3 lastCameraPosition; 
    void Start()
    {
    
        lastCameraPosition = cameraTransform.position;
    }
    void Update()
    {
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaPosition.x * parallaxFactor, deltaPosition.y * parallaxFactor, 0);

        lastCameraPosition = cameraTransform.position;
    }
}