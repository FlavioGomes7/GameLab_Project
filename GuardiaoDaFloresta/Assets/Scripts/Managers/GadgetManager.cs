using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public enum GadgetAlertStage
{
    Paz,
    Ativar

}

public class GadgetManager : MonoBehaviour
{
    [Header("Escolha apenas um tipo de detecção")]
    public bool detectarUmInimigoDeCadaVez;
    public bool detectarTodosOsInimigos;
    public float gadgetFov;
    [Range(0, 360)] public float gadgetFovAngle;
    public GadgetAlertStage gadgetAlertStage;
    [SerializeField][Range(0, 2)] public float gadgetAlertLevel;
    public bool enemyInFOV;
    [Header("O Current Enemy trabalha automaticamente")]
    public GameObject currentEnemy;
    private float reducedNavMeshSpeed = 1f;
    
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gadgetAlertStage = GadgetAlertStage.Paz;
        gadgetAlertLevel = 0;
    }

    public void Update()
    {
        bool enemyInFOV = false;
        if (detectarUmInimigoDeCadaVez)
        {
            Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, gadgetFov);
            foreach (Collider c in targetsInFOV)
            {
                if (c.CompareTag("Enemy"))
                {
                    float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                    if (Mathf.Abs(signedAngle) < gadgetFovAngle / 2)//&& !ObstacleBetween(transform.position, c.transform.position))
                    {
                        enemyInFOV = true;
                        if (currentEnemy == null)
                        {
                            currentEnemy = c.gameObject;

                        }
                        else if (c.transform != currentEnemy)
                        {
                            currentEnemy = c.gameObject;

                        }

                    }
                    break;
                }


            }
        }
        if(detectarTodosOsInimigos)
        {
            Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, gadgetFov);
            foreach (Collider c in targetsInFOV)
            {
                if (c.CompareTag("Enemy"))
                {
                    enemyInFOV = true;
                    
                    NavMeshAgent navAgent = c.GetComponent<NavMeshAgent>();
                    if (navAgent != null)
                    {
                        navAgent.speed = reducedNavMeshSpeed;
                    }
                   



                }
               


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
                    gadgetAlertStage = GadgetAlertStage.Ativar;
                    
                }
                break;
            case GadgetAlertStage.Ativar:
                if (!enemyInFOV)
                {
                   
                    
                    gadgetAlertStage = GadgetAlertStage.Paz;
                        
                    

                }
                break;
                
            
        }
    }


}
