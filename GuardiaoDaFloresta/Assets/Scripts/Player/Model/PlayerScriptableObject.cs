using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerScriptableObject : ScriptableObject
{
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
