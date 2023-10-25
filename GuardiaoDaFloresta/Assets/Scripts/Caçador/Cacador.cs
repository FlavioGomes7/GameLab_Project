using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cacador : MonoBehaviour
{
    public NavMeshAgent cacador;
    public Transform player;
    public GameObject cacadorBullet;
    public Transform spawnPoint;
    public float shootForce;
    EnemyManager enemyManager;

    [SerializeField] private float timer = 5;

    private float bulletTime;

    public float range; 

    public Transform centrePoint;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    void Update()
    {

        if (enemyManager.alertLevel >= 50) 
        {
            Moving();
        }
        if(enemyManager.alertLevel >= 200)
        {
            ShootPlayer();
            Moving();
        }
        else
        {
            if (cacador.remainingDistance <= cacador.stoppingDistance) 
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) 
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                    cacador.SetDestination(point);
                }
            }
        }
        if(enemyManager.shortAlertLevel >= 50)
        {
            Moving();
        }
        else
        {
            if (cacador.remainingDistance <= cacador.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    cacador.SetDestination(point);
                }
            }
        }

       

    }

    private void ShootPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;
        GameObject bulletObj = Instantiate(cacadorBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * shootForce);
        Destroy(bulletObj, 2f );
    }

    private void Moving()
    {
        cacador.SetDestination(player.position);
        
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}
