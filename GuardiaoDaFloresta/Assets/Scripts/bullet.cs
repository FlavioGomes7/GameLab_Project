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
            
            //Debug.Log("acertei");
            PlayerManager hp = collision.GetComponent<PlayerManager>();
            {
                if (hp != null)
                {
                    hp.TakeDamage(bulletDamage);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    


}
