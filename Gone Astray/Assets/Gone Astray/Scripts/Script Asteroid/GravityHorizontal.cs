using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHorizontal : MonoBehaviour
{
    public float gravityStrength = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 downwardDirection = new Vector2(1, 0); 
                playerRb.AddForce(downwardDirection * gravityStrength);
            }
        }
    }
}
