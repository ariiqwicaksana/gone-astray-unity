using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balance : MonoBehaviour
{
    public float targetrotation;
    Rigidbody2D rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetrotation, force * Time.deltaTime));
    }
}
