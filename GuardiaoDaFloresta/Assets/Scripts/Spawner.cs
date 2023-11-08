using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float numEnemies;
    [SerializeField] private float spawnRate;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {

        StartCoroutine(Spawning());

    }

    private IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        for (int i = 0; i < numEnemies; i++) 
        {
           Instantiate(prefabEnemy, spawnPoint);
           yield return wait;        
        }
    }
    
}
