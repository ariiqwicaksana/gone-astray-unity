using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f; 
    public Vector2 direction = new Vector2(1, 0); 
    private Material material; 
    private Vector2 offset; 
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        offset += direction * scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
