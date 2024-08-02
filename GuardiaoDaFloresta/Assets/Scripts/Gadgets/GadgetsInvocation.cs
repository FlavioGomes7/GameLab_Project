using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetsInvocation : MonoBehaviour
{
    public GameObject gadget;
    public GameObject gadgetTwo;
    public Transform spawnGadgets;
    public float gadgetLifetime;
    PlayerManager playerManager;
    public int towerPointsRequired;
    //public bool towerReady;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TowerDefense();
        SlowTower();
    }

    public void TowerDefense()
    {
        if (playerManager.pointsCurrent >= towerPointsRequired)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject newGadget = Instantiate(gadget, spawnGadgets.position, spawnGadgets.rotation);
                playerManager.pointsCurrent -= towerPointsRequired;
                Destroy(newGadget, gadgetLifetime); 
            }
        }
    }

    public void SlowTower()
    {
        if (playerManager.pointsCurrent >= towerPointsRequired)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject newGadget = Instantiate(gadgetTwo, spawnGadgets.position, spawnGadgets.rotation);
                playerManager.pointsCurrent -= towerPointsRequired;
                Destroy(newGadget, gadgetLifetime);
            }
        }
    }
    
}
