using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public enum AlertStage
{
    Paz,
    Curioso,
    Matar,
    Caca

}

public class EnemyManager : MonoBehaviour
{
    Cacador cacador;
    private bool fovAumentado;
    public float cacaDuration = 20; 
    public float cacaTimer;
    public GameObject detectionReference;

    public float fov;
    [Range(0, 360)] public float fovAngle;

    public float shortFov;
    [Range(0, 360)] public float shortFovAngle;


    public AlertStage alertStage;
    [SerializeField][Range(0, 200)] public float alertLevel;

    public AlertStage shortAlertStage;
    [Range(0, 20)] public float shortAlertLevel;




    private void Start()
    {
        cacador = GetComponent<Cacador>();
        fovAumentado = false;
        detectionReference.SetActive(false);

    }

    private void Awake()
    {
        alertStage = AlertStage.Paz;
        alertLevel = 0;
    }

    private void Update()
    {
        bool playerInShortFOV = false;
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position,fov);
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
        Collider[] targetsInShortFOV = Physics.OverlapSphere(transform.position, shortFov);
        foreach (Collider d in targetsInShortFOV)
        {
            if (d.CompareTag("Player"))
            {
                float shortAngle = Vector3.Angle(transform.forward, d.transform.position - transform.position);
                if (Mathf.Abs(shortAngle) < shortFovAngle / 2 && !ObstacleBetween(transform.position, d.transform.position))
                {
                    playerInShortFOV = true;
                }
                break;
            }


        }

        if (alertStage == AlertStage.Matar || alertStage == AlertStage.Caca)
        {
            if (!fovAumentado) 
            {
                fov += 5;
                shortFov += 1;
                fovAumentado = true;
            }
        }
        else
        {
            if (fovAumentado) 
            {
                fov -= 5; 
                shortFov -= 1; 
                fovAumentado = false; 
            }
        }



        UpdateAlertStage(playerInFOV);
        UpdateShortAlertStage(playerInShortFOV);
       
    }

    private void UpdateAlertStage(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Paz:
                detectionReference.SetActive(false);
                if (playerInFOV)
                {
                    alertStage = AlertStage.Curioso;
                    detectionReference.SetActive(true);
                }
                break;
            case AlertStage.Curioso:
                if (playerInFOV)
                {
                    alertLevel++;
                    if (alertLevel >= 200)
                    {
                        alertStage = AlertStage.Matar;
                        detectionReference.SetActive(false);
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
                cacaTimer = 20;
                if (!playerInFOV)
                {
                    alertStage = AlertStage.Caca;
                    alertLevel = 150;
                }
                break;
            case AlertStage.Caca:
                detectionReference.SetActive(false);
                cacaTimer -= Time.deltaTime;
                if (cacaTimer <= 0)
                {
                    alertStage = AlertStage.Paz;
                    alertLevel = 0;
                    cacaTimer = cacaDuration;
                }
                if (playerInFOV)
                {
                    alertLevel++;
                    detectionReference.SetActive(true);
                    if (alertLevel >= 200)
                    {
                        alertStage = AlertStage.Matar;
                        detectionReference.SetActive(false);
                    }
                }
                break;
        }
    }

    private void UpdateShortAlertStage(bool playerInShortFOV)
    {
        switch (shortAlertStage)
        {
            case AlertStage.Paz:
                if (playerInShortFOV)
                {
                    shortAlertLevel++;
                    if(shortAlertLevel >= 20)
                    {
                        shortAlertStage = AlertStage.Curioso;
                    }
                    
                }
                break;
            case AlertStage.Curioso:
                if (!playerInShortFOV)
                {
                    shortAlertLevel--;
                    if(shortAlertLevel <= 20)
                    {
                        shortAlertStage = AlertStage.Paz;
                    }
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

