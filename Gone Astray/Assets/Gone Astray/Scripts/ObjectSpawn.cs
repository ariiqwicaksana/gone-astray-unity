using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject objectToSpawn;  
    public float spawnDistance = 10f; 
    public float despawnDistance = 15f;

    private Vector3 lastSpawnPosition;
    private GameObject lastSpawnedObject; 

    void Start()
    {
        lastSpawnPosition = playerTransform.position;
        SpawnObject();
    }
    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, lastSpawnPosition);

        if (distance >= spawnDistance)
        {
            SpawnObject();
            lastSpawnPosition = playerTransform.position;
        }

        if (lastSpawnedObject != null)
        {
            float despawnDist = Vector3.Distance(playerTransform.position, lastSpawnedObject.transform.position);

            if (despawnDist >= despawnDistance)
            {
                Destroy(lastSpawnedObject);
                Debug.Log("Objek telah dihapus");
            }
        }
    }
    void SpawnObject()
    {
        Vector3 spawnPosition = playerTransform.position + (playerTransform.position - lastSpawnPosition).normalized * spawnDistance;
        lastSpawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
