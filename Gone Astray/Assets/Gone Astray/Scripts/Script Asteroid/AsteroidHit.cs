using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    public GameObject Asteroid;
    public GameObject GameOverPanel;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(true);
            }
            Destroy(other.gameObject); 
        }
    }
}
