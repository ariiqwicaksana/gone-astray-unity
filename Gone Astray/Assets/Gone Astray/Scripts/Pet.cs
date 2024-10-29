using UnityEngine;

public class Pet : MonoBehaviour 
{
    public Transform player;
    public float followSpeed = 3f; 
    public float stoppingDistance = 1.5f;

    void Update() {
    
        float distance = Vector2.Distance(transform.position, player.position);
    
        if (distance > stoppingDistance) {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}