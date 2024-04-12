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
    private float speedRateMax;
    private float dashRedCooldown;
    private float pointsInitMax;
    private float dashNumberMax;

    //In-game stats
    private float hpCurrent;
    private float damageCurrent;
    private float speedCurrent;
    private float speedRateCurrent;
    private float pointsCurrent;

    //Player Movement 
    private Vector3 playerMovement;
    [SerializeField] private float turnSmoothTime;
    private float turnSmoothVelocity;

    //Player Dash
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
        if(inputHandler.dashTriggered)
        {
            StartCoroutine(inputHandler.DashDelay(0.4f));
            StartCoroutine(Dash());
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
        speedCurrent = speedCurrent + dashForce;
        yield return new WaitForSeconds(dashTime);
        speedCurrent = speedCurrent - dashForce;
    }

    void Start()
    {
        //Set dos Componentes
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputHandler = InputHandler.instance;

        //Set dos valores maximos + Possiveis Buffs
        hpMax = playerStats.HpMax;
        damageMax = playerStats.DamageMax;
        speedMax = playerStats.SpeedMax;
        speedRateMax = playerStats.SpeedRateMax;
        dashRedCooldown = playerStats.DashRedCooldown;
        pointsInitMax = playerStats.PointInitMax;
        dashNumberMax = playerStats.DashNumberMax;

        //Set dos valores In-game, que poderam ser alterados.
        hpCurrent = hpMax;
        damageCurrent = damageMax;
        speedCurrent = speedMax;
        speedRateCurrent = speedRateMax;
        pointsCurrent = pointsInitMax;

        //Settings UI
        healthBar.SetMaxHealth(hpMax);

        //Set Respawns
        length = respawns.Length;
    }

    public void Update()
    {
        HandleMovement();
        Debug.Log(speedCurrent);
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
