using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    public GadgetManager gadgetManager;

    private void Start()
    {
       // gadgetManager = GetComponent<GadgetManager>();
    }

    void Update()
    {
        
        
         Vector3 targetPosition = gadgetManager.currentEnemy.transform.position + Vector3.up * 1.5f;
         transform.LookAt(targetPosition);
        
        
    }
}
