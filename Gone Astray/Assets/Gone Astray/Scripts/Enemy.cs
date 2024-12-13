using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 20f; 
    public float orbitRadius = 3f; 
    private float angle; 
    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius; 
        transform.position = new Vector3(x, transform.position.y, z) + target.position;
        transform.LookAt(target);
    }
}
