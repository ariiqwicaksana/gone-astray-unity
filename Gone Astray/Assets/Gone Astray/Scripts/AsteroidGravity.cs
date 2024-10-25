using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGravity : MonoBehaviour
{
    public float gravityStrength = 5f; 
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 direction = (transform.position - other.transform.position).normalized; 
                playerRb.AddForce(direction * gravityStrength);
            }
        }
    }
}
