using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "ScriptableObjects/PlayerStats")]

public class PlayerScriptableObject : ScriptableObject
{

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

    public float HpMax => statList[0].statValue;
    public float DamageMax => statList[0].statValue;
    public float RangeMax => statList[0].statValue;
    public float SpeedMax => statList[0].statValue;
    public float SpeedRateMax => statList[0].statValue;
    public float DashRedCooldown => statList[0].statValue;
    public float PointInitMax => statList[0].statValue;
    public float DashNumberMax => statList[0].statValue;

    public float moneyPlayer;

    

}
