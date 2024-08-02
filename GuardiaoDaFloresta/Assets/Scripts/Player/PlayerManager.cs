using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    //Player Components
    private CharacterController cc;
    private Animator animator;
    private InputHandler inputHandler;

    //Stats
    private float hpMax;
    private float damageMax;
    private float speedMax;
    private float dashRedCooldown;
    private float dashNumberMax;
    private float pointsInitMax;

    //In-game stats
    private float hpCurrent;
    private float damageCurrent;
    private float speedCurrent;
    private float dashCooldown = 1.5f;
    private float dashNumberCurrent;
    public float pointsCurrent {get; set;}

    //Player Movement 
    private Vector3 playerMovement;
    [SerializeField] private float turnSmoothTime;
    private float turnSmoothVelocity;

    //Player Dash
    private float dashDone = 0;
    private bool canDash = true;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;

    //Player Attack
    [SerializeField] private BoxCollider boxAttack;
    [SerializeField] private SphereCollider sphereAttack;
    [SerializeField] private Transform target;
    private float speedForce = 0.1f;

    //UI
    public HealthBar healthBar;

    //Respawns
    public Transform[] respawns;
    public int length;

    //SO
    [SerializeField] private PlayerScriptableObject playerStats;

    [SerializeField] private ScoreSystem scoreSystem;

    //M�todo para Lidar com movimentação do jogador 
    private void HandleMovement()
    {

        playerMovement = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y);
        if(playerMovement.magnitude >= 1)
        {
            HandleDash();
            cc.Move(playerMovement * speedCurrent * Time.deltaTime);
            animator.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Debug.Log(angle);
            //Debug.Log(targetAngle);
        }
        else
        { animator.SetBool("isWalking", false); }
 
    }

    //Método para lidar com o dash do jogador
    private void HandleDash()
    {

        if (inputHandler.dashTriggered && dashDone < dashNumberCurrent && canDash)
        {
            StartCoroutine(inputHandler.Delay(0.1f, "Dash"));
            StartCoroutine(Dash());
            dashDone++;
            return;
        }
        else if(inputHandler.dashTriggered && dashDone >= dashNumberCurrent && canDash)
        {
            StartCoroutine(inputHandler.Delay(dashCooldown, "Dash"));
            dashDone = 0;
            return;
        }
        else
        {return;}
    }

    //Método para lidar com o ataque
    private void HandleAttack()
    {
        if(inputHandler.attackTriggered)
        {
            canDash = false;
            speedCurrent = 0f;
            animator.SetTrigger("Attack");
            StartCoroutine(inputHandler.Delay(0.2f, "Attack"));     
        }
    }

    //Método para impulsionar o player durante a animação de ataque
    public void ImpulseDamage()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speedForce);
    }

    public void CanDash()
    {
        canDash = true;
    }

    //Método para devolver a velocidade ao player ao fim da animação
    public void ChangeSpeed(float refSpeed)
    {
        speedCurrent = refSpeed;
    }

    //Método para ativar os colliders de dano
    public void DealDamage(int attackNumber)
    {
        if(attackNumber == 1)
        {
            boxAttack.enabled = true;
        }
        else
        {
            sphereAttack.enabled = true;
        }
    }

    //Método para desativar os colliders de dano
    public void EndDamage(int attackNumber)
    {
        if (attackNumber == 1)
        {
            boxAttack.enabled = false;
        }
        else
        {
            sphereAttack.enabled = false;
        }
    }

    //M�todo para receber dano e verificar se est� vivo ou morto
    public void TakeDamage(float damage)
    {
        hpCurrent = hpCurrent - damage;
        healthBar.SetCurrentHealth(hpCurrent);
        scoreSystem.hitted.Invoke();
        if (hpCurrent <= 0)
        {
            StartCoroutine(inputHandler.Delay(0.1f, "Move"));
            Death();
        }
       
    }
    //M�todo para excutar a morte
    public void Death()
    {
        scoreSystem.died.Invoke();
        transform.position = respawns[Random.Range(0, length)].position;
        hpCurrent = hpMax;
        healthBar.SetCurrentHealth(hpCurrent);
    }

    IEnumerator Dash()
    {
        inputHandler.SwichInput("Attack", false);
        speedCurrent += dashForce;
        yield return new WaitForSeconds(dashTime);
        speedCurrent -= dashForce;
        inputHandler.SwichInput("Attack", true);
    }

    public void AddPoints(float points)
    {
        pointsCurrent += points;
    }

    void Start()
    {
        //Set dos Componentes
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputHandler = InputHandler.instance;

        //Set dos valores maximos + Upgrades
        hpMax = playerStats.HpMax;
        damageMax = playerStats.DamageMax;
        speedMax = playerStats.SpeedMax;
        dashRedCooldown = playerStats.DashRedCooldown;
        dashNumberMax = playerStats.DashNumberMax;
        pointsInitMax = playerStats.PointInitMax;


        //Set dos valores In-game, que poderam ser alterados.
        hpCurrent = hpMax;
        damageCurrent = damageMax;
        speedCurrent = speedMax;
        dashCooldown -= dashRedCooldown;
        dashNumberCurrent = dashNumberMax;
        pointsCurrent = pointsInitMax;

        //Settings UI
        healthBar.SetMaxHealth(hpMax);
        healthBar.SetCurrentHealth(hpCurrent);

        //Set Respawns
        length = respawns.Length;

    }

    public void Update()
    {
        //Debug.Log(Time.timeScale);
        HandleMovement();
        HandleAttack();
    }

    public void OnTriggerEnter(Collider other)
    {
        Lenhador lenhadorComponent = other.GetComponent<Lenhador>();
        Cacador cacadorComponent = other.GetComponent<Cacador>();

        if (lenhadorComponent != null)
        {
            lenhadorComponent.TakeDamage(damageCurrent);
        }

        if (cacadorComponent != null)
        {
            cacadorComponent.TakeDamage(damageCurrent);
        }
    
    }
    

}
