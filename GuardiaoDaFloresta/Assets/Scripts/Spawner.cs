using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField] private GameObject prefabEnemy;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float countdown;
    [SerializeField] private Wave[] waves;


    [SerializeField] private int currentWaveIndex = 0;


    public void Update()
    {
        countdown -= Time.deltaTime;

        if(currentWaveIndex >= waves.Length)
        {
            Debug.Log("Acabou as Waves");
            return;
        }

        if (countdown <= 0)
        {
            Spawn();
            countdown = waves[currentWaveIndex].timeToNextWave;
        }

    }

    public void Spawn()
    {
        waves[currentWaveIndex].enemiesleft = waves[currentWaveIndex].enemies.Length;
        StartCoroutine(Spawning());

    }

    private IEnumerator Spawning()
    {
        if(currentWaveIndex < waves.Length)
        {
            WaitForSeconds wait = new WaitForSeconds(waves[currentWaveIndex].spawnRate);
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint);
                waves[currentWaveIndex].enemiesleft--;
                yield return wait;
                if (waves[currentWaveIndex].enemiesleft == 0)
                {
                    currentWaveIndex++;
                }
            }
        }
    }

    [System.Serializable]

    public class Wave
    {
        public GameObject[] enemies;
        public float spawnRate;
        public float timeToNextWave;

        /*[HideInInspector]*/ public int enemiesleft;
    }
    
}
