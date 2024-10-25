using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismemberment : MonoBehaviour
{
    public string spikeTag = "Spike";

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(spikeTag))
        {
            DismemberPart(transform);
        }
    }

    void DismemberPart(Transform bodyPart)
    {
        // Disable the copyAnim script on all child parts
        copyAnim[] animScripts = bodyPart.GetComponentsInChildren<copyAnim>();
        foreach (copyAnim animScript in animScripts)
        {
            animScript.Dismember();  // Stop copying rotation
        }

        // Find and destroy all HingeJoint2D components to detach parts
        HingeJoint2D[] joints = bodyPart.GetComponentsInChildren<HingeJoint2D>();
        foreach (HingeJoint2D joint in joints)
        {
            Destroy(joint);
        }

        // Apply force to all parts to simulate detachment
        Rigidbody2D[] rigidbodies = bodyPart.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
        }
    }
}
