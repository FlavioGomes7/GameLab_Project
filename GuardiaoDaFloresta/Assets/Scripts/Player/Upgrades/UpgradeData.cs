using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UpgradeData : MonoBehaviour
{
    [System.Serializable]   
    public class Upgrade
    {
        public string upgradeName;
        public string description;
        public float numberUpgradesMax;
        public int upgradesMadeIt;
        public float costUpgrade;
        public float upgradeAmount;
        public string statToUpgrade;
    }
    

    public abstract void DoUpgrade();
    
}
