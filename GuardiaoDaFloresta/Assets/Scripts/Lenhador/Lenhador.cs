using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Lenhador : MonoBehaviour
{
    //Enemy Movement
    public Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    public float animationDistanceThreshold;
    //Cacador sos;
    public delegate void damage();
    public static event damage onTakeDamage;


    //Enemy stats
    [SerializeField] private int maxHealth;
    public float currentHealth;

    //Enemy UI
    [SerializeField] private Slider slider;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = CloserTree();
        gameManager = GameManager.instance;
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

       

        if (Vector3.Distance(agent.destination, transform.position) <= animationDistanceThreshold)
        {
            anim.SetBool("Attacking", true);



        }
        else
        {
            anim.SetBool("Attacking", false);

        }

      
       
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetCurrentHealth(currentHealth);
        if (currentHealth <= 0)
        {
            LenhadorDie();
            gameManager.OnDeath(100);
        }
        onTakeDamage();
    }

    private void LenhadorDie()
    {
        //anim de morte
        Destroy(gameObject);
        Debug.Log("morri");
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.value = maxHealth;
        slider.maxValue = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }

    private Transform CloserTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        float minDistance = Mathf.Infinity;
        float distance;
        int indexOfCloserTree = 0;
        for (int i = 0; i < trees.Length; i++)
        {

            distance = Vector3.Distance(transform.position, trees[i].transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                indexOfCloserTree = i;

            }

        }
        return trees[indexOfCloserTree].transform;
    }
}
