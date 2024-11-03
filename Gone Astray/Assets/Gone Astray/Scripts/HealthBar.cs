using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class HealthBar : MonoBehaviour
{
public Slider healthSlider;
public Slider easeHealthSlider;
public float maxHealth = 100f;
public float health;
private float lerpSpeed = 0.05f;


void Start()
{
	health = maxHealth;
}
void Update()
{
	if(healthSlider.value != health)
	{
		healthSlider.value = health;
	}
	if(healthSlider.value != easeHealthSlider.value )
	{
		easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed );
	}
}
	public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}