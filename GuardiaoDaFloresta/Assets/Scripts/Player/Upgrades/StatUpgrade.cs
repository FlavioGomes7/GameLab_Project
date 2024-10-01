using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : UpgradeData
{
    [SerializeField] PlayerScriptableObject statPlayer;
    [SerializeField] List<Upgrade> upgradeDatas = new List<Upgrade>();
    private string upgradeKey;

    public override void DoUpgrade()
    {
        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            foreach (var stat in statPlayer.statList)
            {
                if ((stat.statType == upgradeDatas[i].statToUpgrade) && (statPlayer.moneyPlayer >= upgradeDatas[i].costUpgrade) && (upgradeDatas[i].upgradesMadeIt < upgradeDatas[i].numberUpgradesMax))
                {
                    statPlayer.moneyPlayer -= upgradeDatas[i].costUpgrade;
                    upgradeDatas[i].upgradesMadeIt++;
                    stat.statValue += upgradeDatas[i].upgradeAmount;
                    if (upgradeDatas[i].upgradeName == upgradeKey)
                    {
                        PlayerPrefs.SetInt(upgradeKey, upgradeDatas[i].upgradesMadeIt);
                    }
                    else
                    {
                        upgradeKey = upgradeDatas[i].upgradeName;
                        PlayerPrefs.SetInt(upgradeKey, upgradeDatas[i].upgradesMadeIt);
                    }
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

    public void Start()
    {
        foreach(var upgrades in upgradeDatas)
        {
            upgrades.upgradesMadeIt = PlayerPrefs.GetInt(upgrades.upgradeName, 0);
        }
    }

}
