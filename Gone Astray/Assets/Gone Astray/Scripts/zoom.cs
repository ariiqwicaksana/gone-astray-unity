using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class zoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomspeed = 0.1f;
    public float minorthographicsize = 5f;
    public float maxorthographicsize = 20f;
    private float initialtouchdistance;
    private float initialcamerasize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialtouchdistance = Vector2.Distance(touch1.position, touch2.position);
                initialcamerasize = virtualCamera.m_Lens.OrthographicSize;
            }
            else
            {
                float currentTouchDistance = Vector2.Distance(touch1.position, touch2.position);
                float distanceDifference = initialtouchdistance - currentTouchDistance;
                
                float newSize = initialcamerasize + distanceDifference * zoomspeed;
                newSize = Mathf.Clamp(newSize, minorthographicsize, maxorthographicsize);
                
                virtualCamera.m_Lens.OrthographicSize = newSize;
            }
        }
    }
}
