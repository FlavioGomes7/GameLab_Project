using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : UpgradeData
{
    [SerializeField] PlayerScriptableObject statPlayer;
    [SerializeField] List<Upgrade> upgradeDatas = new List<Upgrade>();

    public override void DoUpgrade()
    {
        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            foreach (var stat in statPlayer.statList)
            {
                if ( (stat.statType == upgradeDatas[i].statToUpgrade) && (statPlayer.moneyPlayer >= upgradeDatas[i].costUpgrade) && (upgradeDatas[i].upgradesMadeIt < upgradeDatas[i].numberUpgradesMax) )
                {
                    statPlayer.moneyPlayer -= upgradeDatas[i].costUpgrade;
                    upgradeDatas[i].upgradesMadeIt++;
                    stat.statValue += upgradeDatas[i].upgradeAmount;
                    return;
                }
                else if (stat.statType == upgradeDatas[i].statToUpgrade && statPlayer.moneyPlayer < upgradeDatas[i].costUpgrade)
                {
                    Debug.Log("Recursos Insuficientes");
                    return;
                }
                else if( (stat.statType == upgradeDatas[i].statToUpgrade) && (statPlayer.moneyPlayer >= upgradeDatas[i].costUpgrade) && (upgradeDatas[i].upgradesMadeIt >= upgradeDatas[i].numberUpgradesMax) )
                {
                    Debug.Log("Maximo da melhoria atingido");
                    return;
                }
                else
                {
                    Debug.LogError("ERRO");
                }
                    
            }

        }
        
    }


}
