using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject bossPrefab; 
    public Transform spawnPoint;  
    private bool bossSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bossSpawned)
        {
            StartBossFight();
        }
    }
    private void StartBossFight()
    {
        bossSpawned = true;
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        HealthBar healthBar = boss.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetHealth(100);  
            healthBar.ShowHealthBar(); 
        }
    }
}
