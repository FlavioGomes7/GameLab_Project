using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Lenhador stats = other.GetComponent<Lenhador>();
            {
                if (stats != null)
                {
                    stats.TakeDamage(attackDamage);
                }
            }

        }

    }
}
