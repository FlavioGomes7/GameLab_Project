using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetsInvocation : MonoBehaviour
{
    public GameObject Gadget;
    public Transform spawnGadgets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(Gadget, spawnGadgets.transform.position, spawnGadgets.transform.rotation);
        }
    }
}
