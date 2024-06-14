using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlowTower : MonoBehaviour
{

    public float velocidadeReduzida = 1f;
    public float velocidadeNormal = 3.5f;
    GadgetsInvocation gadgetsInvocation;
    

    // Start is called before the first frame update
    void Start()
    {
        gadgetsInvocation = GetComponent<GadgetsInvocation>();
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

    private void OnDestroy()
    {
        Collider[] Colliders = Physics.OverlapSphere(gameObject.transform.position, 3);
        foreach (Collider other in Colliders) 
        {
            if(other.gameObject.tag == "Enemy")
            {
                NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
                if (navAgent != null)
                {
                    navAgent.speed = velocidadeNormal;
                }

            }
        }
    }

}
