using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float dashCooldown = 2f;
    private float dashNumberCurrent;
    private float pointsCurrent;

    //Player Movement 
    private Vector3 playerMovement;
    [SerializeField] private float turnSmoothTime;
    private float turnSmoothVelocity;

    //Player Dash
    private float dashDone = 0;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;

    //Player Attack
    public bool canReceiveInput;
    public bool inputReceived;

    //UI
    public HealthBar healthBar;

    //Respawns
    public Transform[] respawns;
    public int length;


    //SO
    [SerializeField] private PlayerScriptableObject playerStats;

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

        }
        else
        { animator.SetBool("isWalking", false); }
       
    }

    private void HandleDash()
    {
 
        if (inputHandler.dashTriggered && dashDone < dashNumberCurrent)
        {
            StartCoroutine(inputHandler.Delay(0.1f, "Dash"));
            StartCoroutine(Dash());
            dashDone++;
            return;
        }
        else if(inputHandler.dashTriggered && dashDone >= dashNumberCurrent)
        {
            StartCoroutine(inputHandler.Delay(dashCooldown, "Dash"));
            dashDone = 0;
            return;
        }
        else
        {return;}
    }

    //M�todo para receber dano e verificar se est� vivo ou morto
    public void TakeDamage(float damage)
    {
        hpCurrent = hpCurrent - damage;
        healthBar.SetCurrentHealth(hpCurrent);
        if (hpCurrent <= 0)
        {
           
            Death();
        }
       
    }
    //M�todo para excutar a morte
    public void Death()
    {
        transform.position = respawns[Random.Range(0, length)].position;
        hpCurrent = hpMax;
        healthBar.SetCurrentHealth(hpMax);
    }

    IEnumerator Dash()
    {
        speedCurrent += dashForce;
        yield return new WaitForSeconds(dashTime);
        speedCurrent -= dashForce;
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

        //Set Respawns
        length = respawns.Length;
    }

    public void Update()
    {
        HandleMovement();
        Debug.Log(dashCooldown);
    }

    public void OnTriggerEnter(Collider other)
    {
        Lenhador lenhadorComponent = other.GetComponent<Lenhador>();
        Cacador cacadorComponent = other.GetComponent<Cacador>();

        if (lenhadorComponent != null)
        {
            lenhadorComponent.TakeDamage(damageCurrent);
        }
        else { return;}

        if (cacadorComponent != null)
        {
            cacadorComponent.TakeDamage(damageCurrent);
        }
        else { return;}
    
    }
    

}
