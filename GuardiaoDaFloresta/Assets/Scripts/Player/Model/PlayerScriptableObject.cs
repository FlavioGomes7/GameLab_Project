using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "ScriptableObjects/PlayerStats")]

public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] private float HpBase;
    [SerializeField] private float damageBase;
    [SerializeField] private float speedBase;
    [SerializeField] private float dashRedBase;
    [SerializeField] private float dashNumberbase;

    public bool isReset = false;
   

    [Serializable]
    public class StatInfo
    {
        public string statType;
        public float statValue;
    }

    [SerializeField] public List<StatInfo> statList = new List<StatInfo>();

    public float GetStat(string status)
    {
        foreach(var s in statList)
        {
            if(s.statType == status)
            {
                return s.statValue;
            }
        }

        Debug.LogError("");
        return 0;
    }

    public void ChangeStat(float amount, int index)
    {
        statList[index].statValue += amount;
    }

    public void ResetStatus()
    {
        if (!isReset)
        {
            foreach (var s in statList)
            {
                s.statValue = 0;
                moneyPlayer = 0;
                isReset = true;
            }
        }
        else
        { return;}
    }

    public float HpMax => statList[0].statValue + HpBase;
    public float DamageMax => statList[1].statValue + damageBase;
    public float SpeedMax => statList[2].statValue + speedBase;
    public float DashRedCooldown => statList[3].statValue + dashRedBase;
    public float DashNumberMax => statList[4].statValue + dashNumberbase;
    public float PointInitMax => statList[5].statValue;

    public float moneyPlayer;

    

}
