using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Stats
    private float hpMax;
    private float damageMax;
    private float speedMax;
    private float speedRateMax;
    private float dashRedCooldown;
    private int pointsInitMax;
    private int dashNumberMax;

    //In-game stats
    private float hpCurrent;
    private float damageCurrent;
    private float speedCurrent;
    private float speedRateCurrent;
    private int pointsCurrent;

    //UI
    public HealthBar healthBar;

    //Respawns
    public Transform[] respawns;
    public int length;


    //SO
    [SerializeField] private PlayerScriptableObject playerStats;


    //M�todo para receber dano e verificar se est� vivo ou morto
    public void TakeDamage(float damage)
    {
        
        if (hpCurrent > 0)
        {
            hpCurrent = hpCurrent - damage;
            healthBar.SetCurrentHealth(hpCurrent);
        }
        else
        {
            Death();
        }
    }
    //M�todo para excutar a morte
    public void Death()
    {
        transform.position = respawns[Random.Range(0, length)].position;
        hpCurrent = hpMax;
    }

    void Start()
    {
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
