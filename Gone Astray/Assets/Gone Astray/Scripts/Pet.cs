using UnityEngine;

public class Pet : MonoBehaviour 
{
    public Transform player;
    public float followSpeed = 3f; 
    public float stoppingDistance = 1.5f; 
    public float maxDistance = 5f;
    public float maxSpeedMultipler = 2f;
    private float originalStoppingDistance; 
    private bool isDistanceReduced = false; 
    void Start() {
        originalStoppingDistance = stoppingDistance; 
    }
    void Update() {
        float distance = Vector2.Distance(transform.position, player.position);
        float currentSpeed = followSpeed;

        if (distance < currentSpeed) {
            currentSpeed *= maxSpeedMultipler;
        }

        if (distance > stoppingDistance) {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            ToggleStoppingDistance(); 
        }
    }
    void ToggleStoppingDistance() {
        if (isDistanceReduced) {
            stoppingDistance = originalStoppingDistance;
        } else {
            stoppingDistance = -1f; 
        }

        isDistanceReduced = !isDistanceReduced; 
    }
}
