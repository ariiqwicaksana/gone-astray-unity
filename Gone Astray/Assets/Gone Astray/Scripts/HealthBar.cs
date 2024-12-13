using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public TMP_Text bossNameText;
    private float health;
    private float lerpSpeed = 1f;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = health; 
        easeHealthSlider.maxValue = maxHealth; 
        easeHealthSlider.value = health;
        if (healthSlider.gameObject.activeSelf)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            easeHealthSlider.gameObject.SetActive(true);
        }
        HideHealthBar();
    }

    void Update()
    {
        healthSlider.value = health;
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0); 
        healthSlider.value = health;

        if (health <= 0)
        {
            Die();
        }
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthSlider.value = health; 
        if (health > 0)
        {
            ShowHealthBar(); 
        }
    }
    private void Die()
    {
        HideHealthBar(); 
    }

    public void HideHealthBar()
    {
        healthSlider.gameObject.SetActive(false);
        easeHealthSlider.gameObject.SetActive(false);
        bossNameText.gameObject.SetActive(false);
    }

    public void ShowHealthBar()
    {
        healthSlider.gameObject.SetActive(true);
        easeHealthSlider.gameObject.SetActive(true);
        bossNameText.gameObject.SetActive(true);
    }
}
