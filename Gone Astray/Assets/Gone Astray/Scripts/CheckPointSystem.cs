using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    public Transform RespawnPoint;
    public GameObject Player;
    public GameObject gameOverPanel;
    SpriteRenderer spriteRenderer; 
    public Sprite passive, active;
    public float respawnDuration = 1f;
    private bool isPlayerDead = false;

    private void Awake()
    {
        playerRb = Player.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
        spriteRenderer.sprite = passive; 
    }

    public void Respawn()
    {
        if (isPlayerDead) 
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpdateCheckpoint(RespawnPoint.position); 
            spriteRenderer.sprite = active; 
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        Player.SetActive(false); 
        yield return new WaitForSeconds(respawnDuration); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void MarkPlayerAsDead()
    {
        isPlayerDead = true;
    }
}
