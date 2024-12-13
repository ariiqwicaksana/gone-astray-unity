using UnityEngine;

public class AsteroidBoundOfArray : MonoBehaviour
{
    public GameObject Asteroid;
    public GameObject Player;
    public float Speed;
    private bool isMoving = false; 

    private void Update()
    {
        if (isMoving)
        {
            Asteroid.transform.position = Vector3.MoveTowards(Asteroid.transform.position, Player.transform.position, Speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
}
