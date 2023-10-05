using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlertStage
{
    Paz,
    Curioso,
    Matar
}

public class EnemyManager : MonoBehaviour
{
    public float fov;
    [Range(0, 360)] public float fovAngle;

    public AlertStage alertStage;
    [Range(0, 300)] public float alertLevel;

    private void Awake()
    {
        alertStage = AlertStage.Paz;
        alertLevel = 0;
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Player"))
            {
                float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                if(Mathf.Abs(signedAngle) < fovAngle / 2 && !ObstacleBetween(transform.position, c.transform.position))
                {   
                  playerInFOV = true;   
                }
                break;
            }
        }

        UpdateAlertStage(playerInFOV);
    }

    private void UpdateAlertStage(bool playerInFOV)
    {
        switch(alertStage)
        {
                case AlertStage.Paz:
                if (playerInFOV)
                {
                    alertStage = AlertStage.Curioso;
                }
                break;
                case AlertStage.Curioso:
                if(playerInFOV)
                {
                    alertLevel++;
                    if(alertLevel >= 300)
                    {
                        alertStage = AlertStage.Matar;
                    }
                        
                }
                else
                {
                    alertLevel--;
                    if (alertLevel <= 0)
                    {
                        alertStage = AlertStage.Paz;
                    }
                        
                }
                break;
                case AlertStage.Matar:
                if (!playerInFOV)
                {
                    alertStage = AlertStage.Curioso;
                } 
                break;

        }
    }

    private bool ObstacleBetween(Vector3 start, Vector3 end)
    {
       
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        Ray ray = new Ray(start, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                
                return true;
            }
        }

        return false;
    }
}

