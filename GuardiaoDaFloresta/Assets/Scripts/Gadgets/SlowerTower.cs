using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerTower : MonoBehaviour
{
    
    private float reducedSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            NavMeshAgent navAgent = other.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.speed = reducedSpeed;
            }



        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            
        }
    }
}
