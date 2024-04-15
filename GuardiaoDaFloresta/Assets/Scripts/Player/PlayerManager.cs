using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
    private float dashCooldown = 2f;
    private float speedCurrent;
    private float speedRateCurrent;
    private float pointsCurrent;

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
            StartCoroutine(inputHandler.Delay(0.1f, "Dash"));
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
        healthBar.SetCurrentHealth(hpCurrent);
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
        pointsCurrent = pointsInitMax;

        //Settings UI
        healthBar.SetMaxHealth(hpMax);

        //Set Respawns
        length = respawns.Length;
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
        else { return; }
    

    }
    

}
