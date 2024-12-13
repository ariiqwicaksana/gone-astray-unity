using UnityEngine;
using System.Collections;
public class Swirl : MonoBehaviour
{
    public float upwardForce = 50f;           
    public float forceDuration = 5f;          
    private Rigidbody2D playerRb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                StartCoroutine(LaunchUpwards()); 
            }
        }
    }
    private IEnumerator LaunchUpwards()
    {
        float startTime = Time.time;

        while (Time.time < startTime + forceDuration)
        {
            playerRb.AddForce(Vector2.up * upwardForce);
            yield return null;
        }
    }
}
