using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class AsteroidSpawner : MonoBehaviour
{
    public Vector2[] points;
    public float speed = 5f;
    public GameObject gameOverPanel;

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
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pemain mati!"); 
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            Destroy(other.gameObject); 
        }
    }
}
