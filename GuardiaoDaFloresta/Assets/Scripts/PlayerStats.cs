using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public int length;
    public Transform[] respawns;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        length = respawns.Length;
    }

    public void MakeDammage(int dammage)
    {
        currentHealth = currentHealth - dammage;

        healthBar.SetCurrentHealth(currentHealth);
    }

    void Update()
    {

        if (currentHealth <= 0)
        {
           transform.position = respawns[Random.Range(0, length)].position;
           currentHealth = maxHealth;
        }
    }
}