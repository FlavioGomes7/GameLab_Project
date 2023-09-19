using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    public float animationDistanceThreshold = 4f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        if (agent.velocity.magnitude > 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        float distanceToTarget = worldDeltaPosition.magnitude;

        if (distanceToTarget <= animationDistanceThreshold)
        {
            anim.SetTrigger("AttackTrigger");



        }
        else
        {
            anim.SetTrigger("AttackTrigger");

        }

        transform.position = agent.nextPosition;
    }

    
}
