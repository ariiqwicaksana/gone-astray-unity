using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject FuelPrefabs; 
    public float spawnInterval = 2.0f;      
    public float spawnBufferDistance = 10.0f;
    public float spawnRangeY = 5.0f;         

    private Transform cameraTransform;
    private Camera mainCamera;

    private void Start()
    {
        cameraTransform = Camera.main.transform; 
        mainCamera = Camera.main;                
        StartCoroutine(SpawnFuel());
    }

    private IEnumerator SpawnFuel()
    {
        while (true)
        {
            Vector3 spawnPosition = GetSpawnPositionOutsideCamera();
            Instantiate(FuelPrefabs, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private Vector3 GetSpawnPositionOutsideCamera()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        int spawnSide = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        switch (spawnSide)
        {
            case 0: 
                spawnPosition = new Vector3(
                    cameraTransform.position.x + Random.Range(-cameraWidth / 2, cameraWidth / 2),
                    cameraTransform.position.y + cameraHeight / 2 + spawnBufferDistance,
                    0f);
                break;
            case 1: 
                spawnPosition = new Vector3(
                    cameraTransform.position.x + Random.Range(-cameraWidth / 2, cameraWidth / 2),
                    cameraTransform.position.y - cameraHeight / 2 - spawnBufferDistance,
                    0f);
                break;
            case 2:
                spawnPosition = new Vector3(
                    cameraTransform.position.x - cameraWidth / 2 - spawnBufferDistance,
                    cameraTransform.position.y + Random.Range(-cameraHeight / 2, cameraHeight / 2),
                    0f);
                break;
            case 3: 
                spawnPosition = new Vector3(
                    cameraTransform.position.x + cameraWidth / 2 + spawnBufferDistance,
                    cameraTransform.position.y + Random.Range(-cameraHeight / 2, cameraHeight / 2),
                    0f);
                break;
        }
        return spawnPosition;
    }
}
