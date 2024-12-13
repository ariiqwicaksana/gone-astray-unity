using UnityEngine;

public class ObjectActive : MonoBehaviour
{
    public GameObject childObject; 

    private void Start()
    {
        if (childObject != null)
        {
            childObject.SetActive(false);
            Debug.Log(childObject.name + " tidak aktif di awal.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (childObject != null)
            {
                childObject.SetActive(true); 
                Debug.Log(childObject.name + " aktif di hirarki.");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (childObject != null)
            {
                childObject.SetActive(false);
                Debug.Log(childObject.name + " tidak aktif di hirarki.");
            }
        }
    }
}
