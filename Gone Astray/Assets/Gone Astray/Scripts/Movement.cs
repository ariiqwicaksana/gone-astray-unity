using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float tarikanKecepatan = 2f; 
    private Rigidbody2D rb;
    private Vector2 movement;

    private bool inAreaPanas = false;
    private bool inAreaDingin = false;


    public List<Transform> areaPanasList = new List<Transform>();
    public List<Transform> areaDinginList = new List<Transform>(); 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    
    void Update()
    {
    
        movement = Vector2.zero;

    
        if (Input.GetKey(KeyCode.W)) 
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            movement.x = 1;
        }

        
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }

    
    void FixedUpdate()
{

    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
}
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AreaPanas"))
        {
            inAreaPanas = true;
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = true;
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AreaPanas"))
        {
            inAreaPanas = false;
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = false;
        }
    }
}
