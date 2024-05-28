using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootTower : MonoBehaviour
{
    //[SerializeField] private float bulletTime;
    public GameObject towerBullet;
    public Transform spawnBullet;
    public float bulletSpeed;
    public float bulletCooldown = 0.5f;
    //[SerializeField] private float timer;

    private void Start()
    {
        bulletCooldown = 0.0f;
    }

    public List<GameObject> enemies = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            
            enemies.Add(other.gameObject);
            other.gameObject.GetComponent<Lenhador>().towers.Add(this.gameObject);
         
   

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Lenhador>().towers.Remove(this.gameObject);
            enemies.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        
        if (enemies.Count > 0)
        {
            bulletCooldown -= Time.deltaTime;
            if (bulletCooldown <= 0.0f)
            {
                bulletCooldown = 0.5f;
                MirareAtirar();

            }
        }
        
        
    }

    private void ShootEnemies()
    {
        Debug.Log("atirar");
        //bulletTime -= Time.deltaTime;
        //if (bulletTime > 0) return;
        GameObject bulletObject = Instantiate(towerBullet, spawnBullet.transform.position, spawnBullet.transform.rotation) as GameObject;
        Rigidbody bulletControl = bulletObject.GetComponent<Rigidbody>();
        bulletControl.AddForce(bulletControl.transform.forward * bulletSpeed);
        Destroy(bulletObject, 2f);
        //bulletTime = timer;

    }

    private void MirareAtirar()
    {
       
        spawnBullet.transform.LookAt(enemies[0].transform);
        ShootEnemies();
        
    }

   
}
