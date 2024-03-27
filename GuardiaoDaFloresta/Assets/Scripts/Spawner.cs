using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    private GameManager gameManager;

    //[SerializeField] private GameObject prefabEnemy;

    [SerializeField] private float countdown;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private Wave[] waves;
    [SerializeField] private int nextWave = 0;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    [System.Serializable]
    public class Enemy
    {
        public GameObject enemy;
        public Transform spawnPoint;
        public int index;
        public int count;
        public float rate;
        public float waveRate;
    }


    [System.Serializable]
    public class Wave
    {
        public string name;
        public Enemy[] enemies;

    }

    public void Start()
    {
        countdown = timeBetweenWaves;
        gameManager = GameManager.instance;
    }


    public void Update()
    {
 
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
                StartCoroutine(SpawnWave(waves[nextWave]) );
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
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Cacador").Length == 0)
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
        foreach (Enemy v in _wave.enemies) 
        {
            StartCoroutine( SpawnEnemy(_wave.enemies, v.index, v.rate) );
            yield return new WaitForSeconds(v.waveRate);
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        state = SpawnState.WAITING;

    }

    private IEnumerator SpawnEnemy(Enemy[] _enemy, int index, float _rate)
    {
        for(int i = 0; i < _enemy[index].count; i++)
        {
            Instantiate(_enemy[index].enemy, _enemy[index].spawnPoint.position, _enemy[index].spawnPoint.rotation);
            //_enemy[index].enemy.GetComponent<Lenhador>().target = ;
            Debug.Log("Spawning Enemy: " + _enemy[index].enemy.name);
            yield return new WaitForSeconds(_rate);
        }
        
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
