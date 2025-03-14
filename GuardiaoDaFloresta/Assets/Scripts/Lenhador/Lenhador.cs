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
    public NavMeshAgent agent;
    private Animator anim;
    public float animationDistanceThreshold;
    //Cacador sos;
    public delegate void damage();
    public static event damage onTakeDamage;


    //Enemy stats
    [SerializeField] private int maxHealth;
    public float currentHealth;
    private GameManager gameManager;


    //Enemy UI
    [SerializeField] private Slider slider;


    public List<GameObject> towers = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        Destroy(gameObject);
        foreach (var tower in towers)
        {
            tower.GetComponent<ShootTower>().enemies.Remove(gameObject);
        }

        
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
}
