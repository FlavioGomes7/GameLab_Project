using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private PlayerScriptableObject playerStats;
    private float damageCurrent;
    private float damageMax;

    public void Start()
    {
        damageMax = playerStats.DamageMax;
        damageCurrent = damageMax;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Enemy"))
        {
           other.GetComponent<Lenhador>().TakeDamage(damageCurrent);
        }

    }

}
