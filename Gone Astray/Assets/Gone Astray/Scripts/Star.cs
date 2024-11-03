using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 0f; 
    private Transform cameraTransform; 

    private void Start()
    {
        cameraTransform = Camera.main.transform; 
    }

    void Update()
    {
        
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < cameraTransform.position.y - 20f)
        {
            Destroy(gameObject); 
        }
    }
}
