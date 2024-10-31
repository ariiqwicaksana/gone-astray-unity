using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensity : MonoBehaviour
{
    public float minIntensity = 1f;      // Minimum intensity
    public float maxIntensity = 10f;     // Maximum intensity
    public float speed = 2f;             // Speed of the oscillation

    private Light2D light2D;
    private float direction = 1f;        // Direction for the intensity change

    void Start()
    {
        // Get the Light2D component
        light2D = GetComponent<Light2D>();
        light2D.intensity = minIntensity; // Start from minimum intensity
    }

    void Update()
    {
        if (light2D != null)
        {
            // Update intensity in the current direction
            light2D.intensity += direction * speed * Time.deltaTime;

            // Reverse direction if we hit max or min intensity
            if (light2D.intensity >= maxIntensity || light2D.intensity <= minIntensity)
            {
                direction *= -1;
                light2D.intensity = Mathf.Clamp(light2D.intensity, minIntensity, maxIntensity);
            }
        }
    }
}
