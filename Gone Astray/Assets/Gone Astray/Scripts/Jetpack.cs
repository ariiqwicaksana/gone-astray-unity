using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Jetpack : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public float forceAmount;
    public float maxfuel = 100f;
    public float consumtion = 10f;
    public Slider fuelbar;
    public Image fuelfill;
    public CinemachineVirtualCamera virtualCamera;
    public float shakeAmplitudeGain = 2.0f;
    public float shakeFrequencyGain = 2.0f;
    public float shakeDuration = 0.5f;
    public Volume volume;
    private MotionBlur motionBlur;
    public float motionBlurIntensity = 0.8f;
    public float resetSpeed = 2f;

    private ParticleSystem.EmissionModule em;
    private CinemachineBasicMultiChannelPerlin noise;
    private float defaultAmplitude;
    private float defaultFrequency;
    private float currentAmplitude;
    private float currentFrequency;
    private float currentBlur;
    [SerializeField] private float currentfuel;


    private CanvasGroup fuelBarCanvasGroup;

    void Start()
    {
        em = ps.emission;

    
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            defaultAmplitude = noise.m_AmplitudeGain;
            defaultFrequency = noise.m_FrequencyGain;
        }

        
        if (volume != null && volume.profile.TryGet(out motionBlur))
        {
            currentBlur = motionBlur.intensity.value;
        }
        currentfuel = currentfuel;

        if (fuelbar != null)
        {
            fuelbar.maxValue = maxfuel;
            fuelbar.value = currentfuel;
        }

        fuelBarCanvasGroup = fuelbar.GetComponent<CanvasGroup>();
        fuelBarCanvasGroup.alpha = 0; 
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1.5f, 0, 0));
        fuelbar.transform.position = screenPos;
        if (fuelbar != null)
        {
            fuelbar.value = currentfuel;
        }

        if (Input.GetKey(KeyCode.Space) && currentfuel > 0)
        {
            currentfuel -= consumtion * Time.deltaTime;

            if (currentfuel < 0)
            {
                currentfuel = 0;
            }

            if (fuelbar != null)
            {
                fuelbar.value = currentfuel;
                fuelBarCanvasGroup.alpha = 1f;
                StopCoroutine(FadeFuelBar(0f));
            }

            rb.AddForce(transform.up * forceAmount);
            em.enabled = true;

            if (noise != null)
            {
                noise.m_AmplitudeGain = shakeAmplitudeGain;
                noise.m_FrequencyGain = shakeFrequencyGain;
            }

            if (motionBlur != null)
            {
                motionBlur.intensity.value = motionBlurIntensity;
            }
        }
        else
        {
            em.enabled = false;

            if (noise != null)
            {
                currentAmplitude = Mathf.Lerp(currentAmplitude, defaultAmplitude, Time.deltaTime * resetSpeed);
                currentFrequency = Mathf.Lerp(currentFrequency, defaultFrequency, Time.deltaTime * resetSpeed);
                noise.m_AmplitudeGain = currentAmplitude;
                noise.m_FrequencyGain = currentFrequency;
            }

            if (motionBlur != null)
            {
                currentBlur = Mathf.Lerp(motionBlur.intensity.value, 0f, Time.deltaTime * resetSpeed);
                motionBlur.intensity.value = currentBlur;
            }

            StartCoroutine(FadeFuelBar(0f));
        }
        UpdatefuelfillAlpha();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FuelPickup")) 
        {
            currentfuel += 40f; 
            if (currentfuel > maxfuel)
            {
                currentfuel = maxfuel;
            }
            fuelbar.value = currentfuel;
            fuelBarCanvasGroup.alpha = 1f; 
            StopCoroutine(FadeFuelBar(0f));
            StartCoroutine(FadeFuelBar(0f, 2f));
            Destroy(collision.gameObject); 
        }
    }

    void UpdatefuelfillAlpha()
    {
        if (fuelfill != null)
        {
            Color fillcolor = fuelfill.color;

            if (currentfuel <= 0)
            {
                fuelbar.fillRect.gameObject.SetActive(false);
            }
            else
            {
                fuelbar.fillRect.gameObject.SetActive(true);
            }

            fuelfill.color = fillcolor;
        }
    }

    private IEnumerator FadeFuelBar(float targetAlpha, float duration = 0.2f)
    {
        float startAlpha = fuelBarCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fuelBarCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        fuelBarCanvasGroup.alpha = targetAlpha;
    }
}
