using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerTower : MonoBehaviour
{
    GadgetManager gadgetManager;
    private float reducedNavMeshSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gadgetManager = GetComponent<GadgetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DoSlow(gadgetManager.targetsInFOV);
    }

    public void DoSlow(Collider[] targetsInFOV)
    {
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Enemy"))
            {
                //enemyInFOV = true;

                NavMeshAgent navAgent = c.GetComponent<NavMeshAgent>();
                if (navAgent != null)
                {
                    navAgent.speed = reducedNavMeshSpeed;
                }




            }



        }
    }
}
