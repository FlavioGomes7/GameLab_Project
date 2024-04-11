using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderRook : MonoBehaviour
{
    [SerializeField]private float bulletTime;
    public GameObject towerBullet;
    public Transform spawnBullet;
    public float bulletSpeed;
    [SerializeField]private float timer;
    GadgetManager gadgetManager;
    // Start is called before the first frame update
    void Start()
    {
        gadgetManager = GetComponent<GadgetManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gadgetManager.currentEnemy != null)
        {
            /*Vector3 directionToEnemy = gadgetManager.currentEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion yRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, yRotation, Time.deltaTime * 360f);*/
            ShootEnemies();
        }
    }

    private void ShootEnemies()
    {

            bulletTime -= Time.deltaTime;
            if (bulletTime > 0) return;
            GameObject bulletObject = Instantiate(towerBullet, spawnBullet.transform.position, spawnBullet.transform.rotation) as GameObject;
            Rigidbody bulletControl = bulletObject.GetComponent<Rigidbody>();
            bulletControl.AddForce(bulletControl.transform.forward * bulletSpeed);
            Destroy(bulletObject, 2f);
            bulletTime = timer;
        
    }
}
