using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damageAmount = 20f; 
    void OnCollisionEnter2D(Collision2D collision)
    { 
        HealthBar targetHealth = collision.gameObject.GetComponent<HealthBar>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damageAmount); 
        }
    }
}