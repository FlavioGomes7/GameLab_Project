using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private PlayerScriptableObject playerStats;
    private float damageCurrent;
    private float damageMax;
    private float attackRangeMax;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public void Start()
    {
        attackRangeMax = playerStats.RangeMax;
        damageMax = playerStats.DamageMax;
        damageCurrent = damageMax;
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Enemy"))
        {
           other.GetComponent<Lenhador>().TakeDamage(damageCurrent);
        }

    }

}
