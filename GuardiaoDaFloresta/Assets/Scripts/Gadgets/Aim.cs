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
        transform.LookAt(gadgetManager.currentEnemy.position);
    }
}
