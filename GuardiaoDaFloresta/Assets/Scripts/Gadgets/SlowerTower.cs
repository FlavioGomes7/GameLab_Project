using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerTower : MonoBehaviour
{
    
    public float velocidadeReduzida = 1f;
    public float  velocidadeNormal = 3.5f;
    

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
                navAgent.speed = velocidadeReduzida;
            }



        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            NavMeshAgent navAgent = other.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.speed = velocidadeNormal;
            }
        }
    }
}
