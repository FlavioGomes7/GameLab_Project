using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum GadgetAlertStage
{
    Paz,
    Atirar

}

public class GadgetManager : MonoBehaviour
{
    
    public float gadgetFov;
    [Range(0, 360)] public float gadgetFovAngle;
    public GadgetAlertStage gadgetAlertStage;
    [SerializeField][Range(0, 2)] public float gadgetAlertLevel;
    public bool enemyInFOV;
    public Transform currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gadgetAlertStage = GadgetAlertStage.Paz;
        gadgetAlertLevel = 0;
    }

    private void Update()
    {
        
        bool enemyInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, gadgetFov);
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Enemy"))
            {
                float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < gadgetFovAngle / 2 )//&& !ObstacleBetween(transform.position, c.transform.position))
                {
                    enemyInFOV = true;
                    if (currentEnemy == null)
                    {
                        currentEnemy = c.transform;
                    }
                    else if (c.transform != currentEnemy)
                    {
                        currentEnemy = c.transform;
                    }

                }
                break;
            }


        }

        UpdateGadgetAlertStage(enemyInFOV);
       

    }

    private void UpdateGadgetAlertStage(bool enemyInFOV)
    {
        switch (gadgetAlertStage)
        {
            case GadgetAlertStage.Paz:
               
                if (enemyInFOV)
                {
                    gadgetAlertStage = GadgetAlertStage.Atirar;
                    
                }
                break;
            case GadgetAlertStage.Atirar:
                if (!enemyInFOV)
                {
                   
                    
                    gadgetAlertStage = GadgetAlertStage.Paz;
                        
                    

                }
                break;
                
            
        }
    }

}
