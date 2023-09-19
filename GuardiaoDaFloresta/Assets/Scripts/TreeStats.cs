using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void MakeDammage(int dammage)
    {
        currentHealth = currentHealth - dammage;

        healthBar.SetCurrentHealth(currentHealth);
    }
}