using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public float forceAmount;

    private ParticleSystem.EmissionModule em;

    // Start is called before the first frame update
    void Start()
    {
        em = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply force in the direction the object is facing (based on rotation)
            rb.AddForce(transform.up * forceAmount); // Move in the 'up' direction relative to the rotation
            em.enabled = true; // Enable particle effect
        }
        else
        {
            em.enabled = false; // Disable particle effect
        }
    }
}
