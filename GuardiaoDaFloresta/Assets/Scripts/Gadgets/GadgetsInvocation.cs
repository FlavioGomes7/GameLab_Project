using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetsInvocation : MonoBehaviour
{
    public GameObject Gadget;
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
    }

    public void TowerDefense()
    {
        if (playerManager.pointsCurrent >= towerPointsRequired)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GameObject newGadget = Instantiate(Gadget, spawnGadgets.position, spawnGadgets.rotation);
                playerManager.pointsCurrent -= towerPointsRequired;
                Destroy(newGadget, gadgetLifetime); 
            }
        }
    }
}
