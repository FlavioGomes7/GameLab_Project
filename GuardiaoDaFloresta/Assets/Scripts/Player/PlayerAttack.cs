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
        damageMax = playerStats.DamageMax;
        damageCurrent = damageMax;
    }

    public void OnTriggerEnter(Collider other)
    {
        Lenhador lenhadorComponent = other.GetComponent<Lenhador>();
        Cacador cacadorComponent = other.GetComponent<Cacador>();

        if (lenhadorComponent != null)
        {
            lenhadorComponent.TakeDamage(damageCurrent);
        }
        if (cacadorComponent != null)
        {
            cacadorComponent.TakeDamage(damageCurrent);
        }
        
        





    }

}
