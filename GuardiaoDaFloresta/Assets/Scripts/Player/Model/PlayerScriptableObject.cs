using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerScriptableObject : ScriptableObject
{

    public class StatInfo
    {
        public PlayerScriptableObject statType;
        public float statValue;
    }
    
    [SerializeField] private List<StatInfo> statInfo = new List<StatInfo>();

    public float GetStat(PlayerScriptableObject stat)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            return s.statValue;
        }

        Debug.LogError($"Valor do stat de {stat} no {this.name} não encontrado");
        return 0;
    }

    [SerializeField] private float hpMax;
    [SerializeField] private float damageMax;
    [SerializeField] private float rangeMax;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedRateMax;
    [SerializeField] private float dashRedCooldown;
    [SerializeField] private int pointsInitMax;
    [SerializeField] private int dashNumberMax;


    public float HpMax => hpMax;
    public float DamageMax => damageMax;
    public float RangeMax => rangeMax;
    public float SpeedMax => speedMax;
    public float SpeedRateMax => speedRateMax;
    public float DashRedCooldown => dashRedCooldown;
    public int PointInitMax => pointsInitMax;
    public int DashNumberMax => dashNumberMax;
}
