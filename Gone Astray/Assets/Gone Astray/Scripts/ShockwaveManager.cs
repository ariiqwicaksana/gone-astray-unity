using System.Collections;
using UnityEngine;

public class ShockwaveManager : MonoBehaviour
{
    [SerializeField] private float shockWaveTime = 0.75f;
    [SerializeField] private float slowShockWaveTime = 2f; 
    private Coroutine shockWaveCoroutine;
    private Material material;
    private static int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CallShockWave(shockWaveTime);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CallShockWave(slowShockWaveTime); 
        }
    }

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void CallShockWave(float duration)
    {
        if (shockWaveCoroutine != null)
        {
            StopCoroutine(shockWaveCoroutine);
        }
        shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f, duration));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos, float duration)
    {
        material.SetFloat(waveDistanceFromCenter, startPos);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / duration));
            material.SetFloat(waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }
    }
}
