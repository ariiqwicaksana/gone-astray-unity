using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class BossMovement : MonoBehaviour
{
    public Vector2[] points;
    public float speed = 5f;

    private int currentPointIndex = 0;
    private void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0];
        }
        else
        {
            Debug.LogWarning("Array points tidak diisi!"); 
        }
    }
    private void Update()
    {
        if (points.Length > 0)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex], step);
            if (Vector2.Distance(transform.position, points[currentPointIndex]) < 0.1f)
            {
                currentPointIndex++;
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0; 
                }
            }
        }
    }
}
