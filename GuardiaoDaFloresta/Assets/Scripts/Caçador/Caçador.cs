using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caçador : MonoBehaviour
{
    public NavMeshAgent cacador;
    public Transform player;
    public GameObject cacadorBullet;
    public Transform spawnPoint;
    public float shootForce;

    [SerializeField] private float timer = 5;

    private float bulletTime;

    void Update()
    {
        
        if(player == null)
        {

        }
        else
        {
            cacador.SetDestination(player.position);
            ShootPlayer();

        }
    }

    void ShootPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;
        GameObject bulletObj = Instantiate(cacadorBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * shootForce);
        Destroy(bulletObj, 1f );
    }
}
