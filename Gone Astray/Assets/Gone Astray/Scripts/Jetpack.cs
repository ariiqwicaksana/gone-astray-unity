using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Jetpack : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public float forceAmount;

    public CinemachineVirtualCamera virtualCamera; // Reference to your Cinemachine virtual camera
    public float shakeAmplitudeGain = 2.0f; // Set desired shake strength
    public float shakeFrequencyGain = 2.0f; // Set desired shake frequency
    public float shakeDuration = 0.5f; // Time for which the camera shake happens

    public Volume volume; // Reference to your global volume with motion blur
    private MotionBlur motionBlur;
    public float motionBlurIntensity = 0.8f; // Set desired motion blur intensity
    public float resetSpeed = 2f; // Speed at which shake and motion blur reset smoothly

    private ParticleSystem.EmissionModule em;
    private CinemachineBasicMultiChannelPerlin noise;
    private float defaultAmplitude;
    private float defaultFrequency;
    private float currentAmplitude;
    private float currentFrequency;
    private float currentBlur;

    // Start is called before the first frame update
    void Start()
    {
        em = ps.emission;

        // Get the noise component from the virtual camera
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            defaultAmplitude = noise.m_AmplitudeGain; // Save the default amplitude
            defaultFrequency = noise.m_FrequencyGain; // Save the default frequency
        }

        // Access the motion blur effect from the volume
        if (volume != null && volume.profile.TryGet(out motionBlur))
        {
            currentBlur = motionBlur.intensity.value; // Store the current motion blur value
        }

        // Set the current values to the default
        currentAmplitude = defaultAmplitude;
        currentFrequency = defaultFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply force in the direction the object is facing (based on rotation)
            rb.AddForce(transform.up * forceAmount); // Move in the 'up' direction relative to the rotation
            em.enabled = true; // Enable particle effect

            // Trigger camera shake (instantly increase)
            if (noise != null)
            {
                noise.m_AmplitudeGain = shakeAmplitudeGain;
                noise.m_FrequencyGain = shakeFrequencyGain;
            }

            // Increase motion blur intensity
            if (motionBlur != null)
            {
                motionBlur.intensity.value = motionBlurIntensity;
            }
        }
        else
        {
            em.enabled = false; // Disable particle effect

            // Smoothly reset camera shake
            if (noise != null)
            {
                currentAmplitude = Mathf.Lerp(currentAmplitude, defaultAmplitude, Time.deltaTime * resetSpeed);
                currentFrequency = Mathf.Lerp(currentFrequency, defaultFrequency, Time.deltaTime * resetSpeed);
                noise.m_AmplitudeGain = currentAmplitude;
                noise.m_FrequencyGain = currentFrequency;
            }

            // Smoothly reset motion blur
            if (motionBlur != null)
            {
                currentBlur = Mathf.Lerp(motionBlur.intensity.value, 0f, Time.deltaTime * resetSpeed);
                motionBlur.intensity.value = currentBlur;
            }
        }
    }
}
