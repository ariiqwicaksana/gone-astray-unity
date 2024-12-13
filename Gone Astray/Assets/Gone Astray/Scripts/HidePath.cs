using System.Collections;
using UnityEngine;

public class HidePath : MonoBehaviour
{
    public GameObject targetObject; 
    public float fadeDuration = 1.5f; 
    private SpriteRenderer targetSpriteRenderer;
    private bool isFading = false;
    private void Start()
    {
        if (targetObject != null)
        {
            targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("Target Object tidak diatur! Harap set di Inspector.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading && targetSpriteRenderer != null)
        {
            StartCoroutine(FadeOut());
        }
    }
    private IEnumerator FadeOut()
    {
        isFading = true;
        float startAlpha = targetSpriteRenderer.color.a;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color color = targetSpriteRenderer.color;
            color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            targetSpriteRenderer.color = color;
            yield return null;
        }
        Color finalColor = targetSpriteRenderer.color;
        finalColor.a = 0;
        targetSpriteRenderer.color = finalColor;
    }
}
