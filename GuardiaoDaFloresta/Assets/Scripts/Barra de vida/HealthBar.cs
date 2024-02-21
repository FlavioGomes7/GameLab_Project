using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealth(float maxHealth)
    {
        slider.value = maxHealth;
        slider.maxValue = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
