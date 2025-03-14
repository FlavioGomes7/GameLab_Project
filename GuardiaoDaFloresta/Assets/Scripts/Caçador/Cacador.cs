using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Cacador : MonoBehaviour
{
    public NavMeshAgent cacador;
    public Transform player;
    public GameObject cacadorBullet;
    public Transform spawnPoint;
    public float shootForce;
    EnemyManager enemyManager;
    public bool atirando;
    Rigidbody rb;
    [SerializeField] private float timer = 5;
    private float velocidadePadrao;
    private float velocidadeRotacaoPadrao;
    public float bulletTime;
    private float maxSpeed = 15f;
    private float maxAngularSpeed = 1000f;
    private float maxAcelerationSpeed = 10f;
    private float minSpeed = 10f;
    private float minAngularSpeed = 400f;
    private float minAcelerationSpeed = 5f;
    public Transform aim;
    public GameObject waypoint;
    private float speed = 10;
    public float aimSpeed;
   




    [SerializeField] private int cacadorMaxHealth;
    public float currentHealth;

 
    [SerializeField] private Slider cacadorSlider;


    public float range;

    public Transform centrePoint;
    public GameManager gameManager;

    private void Start()
    {

        enemyManager = GetComponent<EnemyManager>();
        atirando = false;
        rb = GetComponent<Rigidbody>();
        currentHealth = cacadorMaxHealth;
        SetMaxHealth(cacadorMaxHealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bulletTime = timer;
        Lenhador.onTakeDamage += Moving;
        gameManager = GameManager.instance;

        

    }

    void Update()
    {
        if(cacador.enabled == false)
        {
            ACAMINHO();
        }

        if (cacador.enabled == true)
        {



            if (enemyManager.alertStage == AlertStage.Curioso || enemyManager.shortAlertStage == AlertStage.Curioso)
            {
                Moving();
            }
            if (enemyManager.alertStage == AlertStage.Matar)
            {
                atirando = true;
                ShootPlayer();
                Moving();
                

            }
            else
            {
                bulletTime = timer;
                atirando = false;
                if (cacador.remainingDistance <= cacador.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        cacador.SetDestination(point);
                    }
                }
            }
            if (enemyManager.alertStage == AlertStage.Caca || enemyManager.alertStage == AlertStage.Matar)
            {

                cacador.speed = maxSpeed;
                cacador.angularSpeed = maxAngularSpeed;
                cacador.acceleration = maxAcelerationSpeed;
                if (cacador.remainingDistance <= cacador.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        cacador.SetDestination(point);
                    }
                }

            }
            else
            {
                cacador.speed = minSpeed;
                cacador.angularSpeed = minAngularSpeed;
                cacador.acceleration = minAcelerationSpeed;
            }
            
           

        }


    }

    private void ShootPlayer()
    {
        
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        GameObject bulletObj = Instantiate(cacadorBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * shootForce);
        Destroy(bulletObj, 2f);
        bulletTime = timer;
    }

    public void Moving()
    {

        cacador.SetDestination(player.position);
        Aim();



        if (atirando)
        {
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            Aim();
        }



    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {

            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetCurrentHealth(currentHealth);

        // anim de dano

        if (currentHealth <= 0)
        {
            CacadorDie();
            gameManager.OnDeath(150);
        }

    }

    void CacadorDie()
    {
        //anim de morte
        Destroy(gameObject);
    }

    public void SetMaxHealth(int maxHealth)
    {
        cacadorSlider.value = maxHealth;
        cacadorSlider.maxValue = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        cacadorSlider.value = currentHealth;
    }

    public void ACAMINHO()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);
        transform.position = newPos;
        if (transform.position == waypoint.transform.position)
        {

            cacador.enabled = true;
        }
    }

    private void Aim()
    {
        Quaternion rotTarget = Quaternion.LookRotation(player.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, aimSpeed * Time.deltaTime);
    }
}
