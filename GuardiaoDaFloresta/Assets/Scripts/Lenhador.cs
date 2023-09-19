using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Lenhador : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    public float animationDistanceThreshold;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        

    } 
 
    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            agent.isStopped = true;
            anim.SetBool("IsMoving", false);
            anim.SetBool("Attacking", false);
            return;
        }

        agent.SetDestination(target.position);



        if (agent.velocity.magnitude > 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= animationDistanceThreshold)
        {
            anim.SetBool("Attacking", true);



        }
        else
        {
            anim.SetBool("Attacking", false);

        }

        
    }
}
