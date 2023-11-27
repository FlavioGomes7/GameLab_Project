using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int bulletDamage;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats stats = collision.GetComponent<PlayerStats>();
            {
                if (stats != null)
                {
                    stats.MakeDammage(bulletDamage);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    


}
