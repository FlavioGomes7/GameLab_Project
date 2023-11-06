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

    //SO
    [SerializeField] private PlayerScriptableObject playerStats;

    //M�todo para danificar inimigo
    public float DealDamage(float enemyHp)
    {
        enemyHp -= damageCurrent;
        return enemyHp;
    }

    //M�todo para receber dano e verificar se est� vivo ou morto
    public void TakeDamage(/*float damage*/)
    {
        
        if (hpCurrent > 0)
        {
            //hpCurrent -= damage;
        }
        else
        {
            Death();
        }
    }
    //M�todo para excutar a morte
    public void Death()
    {
        
        Debug.Log("Morreu");
      
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
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Enemy"))
        {
            DealDamage(other.GetComponent<Lenhador>().currentHealth);
        }

    }

}
