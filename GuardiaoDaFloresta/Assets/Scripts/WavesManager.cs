using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersLenhadores;
    [SerializeField] private GameObject[] spawnersCacadores;
    [SerializeField] private int numWave;
    [SerializeField] private float[] waveDuration;
    private float timer;

    void Start()
    {
        numWave = 0;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        Debug.Log(timer);
        StartWave();
    }

    private void StartWave()
    {
        if (numWave == 0)
        {
            foreach(GameObject spawner in spawnersLenhadores)
            {
                if(spawner.CompareTag("1°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }

            foreach(GameObject spawner in spawnersCacadores)
            {
                if(spawner.CompareTag("1°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }
            numWave++;
        }
        else if(numWave == 1 && timer >= waveDuration[numWave - 1])
        {

            foreach (GameObject spawner in spawnersLenhadores)
            {
                if (spawner.CompareTag("2°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }

            foreach (GameObject spawner in spawnersCacadores)
            {
                if (spawner.CompareTag("2°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }
            numWave++;
            timer = 0;
        }
        else if(numWave == 2 && timer >= waveDuration[numWave - 1])
        {
            foreach (GameObject spawner in spawnersLenhadores)
            {
                if (spawner.CompareTag("3°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }

            foreach (GameObject spawner in spawnersCacadores)
            {
                if (spawner.CompareTag("3°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }
            numWave++;
            timer = 0;
        }
        else if(numWave == 3 && timer >= waveDuration[numWave - 1])
        {
            foreach (GameObject spawner in spawnersLenhadores)
            {
                if (spawner.CompareTag("4°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }

            foreach (GameObject spawner in spawnersCacadores)
            {
                if (spawner.CompareTag("4°Wave"))
                {
                    spawner.GetComponent<Spawner>().Spawn();
                }
                else
                {
                    return;
                }
            }
            numWave++;
            timer = 0;
        }
        else
        {
            return;
        }
    }
}
