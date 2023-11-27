using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    private GameManager gameManager;

    //[SerializeField] private GameObject prefabEnemy;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float countdown;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private Wave[] waves;
    [SerializeField] private int nextWave = 0;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;


    [System.Serializable]

    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
        //public GameObject[] enemies;
        //public float spawnRate;
        //public float timeToNextWave;
        ///*[HideInInspector]*/
        //public int enemiesleft;
    }

    public void Start()
    {
        countdown = timeBetweenWaves;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    public void Update()
    {
 
        //if(currentWaveIndex >= waves.Length)
        //{
        //    Debug.Log("Acabou as Waves");
        //    return;
        //}
        if(state == SpawnState.WAITING)
        {
            if(!isEnemiesAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
            
        }

        if (countdown <= 0)
        {
           if(state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave( waves[nextWave] ) );
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }

    }


    bool isEnemiesAlive()
    {
        searchCountDown -= Time.deltaTime;

        if(searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        
        return true;
    }


    private IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++ )
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;








        //if(currentWaveIndex < waves.Length)
        //{
        //    WaitForSeconds wait = new WaitForSeconds(waves[currentWaveIndex].spawnRate);
        //    for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
        //    {
        //        Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint);
        //        waves[currentWaveIndex].enemiesleft--;
        //        yield return wait;
        //        if (waves[currentWaveIndex].enemiesleft == 0)
        //        {
        //            currentWaveIndex++;
        //        }
        //    }
        //}
    }

    private void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        
        state = SpawnState.COUNTING;
        countdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("Voce Venceu");
            gameManager.winCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            nextWave++;
        }

    }


    
}
