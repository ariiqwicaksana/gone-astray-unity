using UnityEngine;

public class BackgroundMoving : MonoBehaviour 
{
    private float startPosX, startPosY, lengthX, lengthY;
    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    void Start()
    {
        
        GameObject backgroundObject = GameObject.Find("Background");
        if (backgroundObject != null)
        {
            SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                startPosX = transform.position.x;
                startPosY = transform.position.y;

                lengthX = spriteRenderer.bounds.size.x;
                lengthY = spriteRenderer.bounds.size.y;
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the Background object!");
            }
        }
        else
        {
            Debug.LogError("Background object not found in the scene!");
        }
    }

    void FixedUpdate() 
    {
        float distanceX = cam.transform.position.x * parallaxEffectX;
        float distanceY = cam.transform.position.y * parallaxEffectY;
        
        float movementX = cam.transform.position.x * (1 - parallaxEffectX);
        float movementY = cam.transform.position.y * (1 - parallaxEffectY);
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
        if (movementX > startPosX + lengthX)
        {
            startPosX += lengthX;
        }    
        else if (movementX < startPosX - lengthX)
        {
            startPosX -= lengthX;
        }
        if (movementY > startPosY + lengthY)
        {
            startPosY += lengthY;
        }
        else if (movementY < startPosY - lengthY)
        {
            startPosY -= lengthY;
        }
    }
}
