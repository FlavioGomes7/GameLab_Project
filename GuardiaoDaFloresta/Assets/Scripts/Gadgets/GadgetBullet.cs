using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetBullet : MonoBehaviour
{
    [SerializeField] private int damage;
   
    public void OnTriggerEnter(Collider collider)
    {
        
        
        Lenhador lenhadorComponent = collider.GetComponent<Lenhador>();
        Cacador cacadorComponent = collider.GetComponent<Cacador>();

        if (lenhadorComponent != null)
        {
            //lenhadorComponent.TakeDamage(damage);
            
            //Destroy(gameObject);
        }
      /*  if (cacadorComponent != null)
        {
            cacadorComponent.TakeDamage(damage);
        }*/


    }
}
