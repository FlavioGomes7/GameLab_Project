using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            TreeStats stats = other.GetComponent<TreeStats>();
            {
                if (stats != null)
                {
                    stats.MakeDammage(damage);
                }
            }

        }

    }
}

