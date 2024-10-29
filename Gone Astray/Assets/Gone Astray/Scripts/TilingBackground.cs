using UnityEngine;

public class TilingBackground : MonoBehaviour
{
    public float width; 
    public Transform playerTransform;
    private void Update()
    {
        if (transform.position.x < playerTransform.position.x - width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}