using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public Transform player;
    public GameObject setaPrefab;
    public float indicatorDistancia = 5f;
    public float ativacaoDistancia = 50f;
    EnemyManager enemyManager;

    private Dictionary<GameObject, GameObject> enemyIndicators = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
       
    }

    private void Update()
    {
        GameObject[] setas = GameObject.FindGameObjectsWithTag("Cacador"); 

          
            foreach (GameObject seta in setas)
            {
               enemyManager = seta.GetComponent<EnemyManager>();
            if (enemyManager.alertStage == AlertStage.Curioso | enemyManager.alertStage == AlertStage.Matar)
            {


                float distancia = Vector3.Distance(player.transform.position, seta.transform.position);
                if (distancia < ativacaoDistancia)
                {
                    Debug.Log("morra");
                    if (!enemyIndicators.ContainsKey(seta))
                    {
                        GameObject newIndicador = Instantiate(setaPrefab, Vector3.zero, Quaternion.identity);
                        enemyIndicators.Add(seta, newIndicador);
                    }

                    Vector3 setaDirecao = (seta.transform.position - player.position).normalized;
                    GameObject indicador = enemyIndicators[seta];
                    indicador.transform.position = player.position + setaDirecao * indicatorDistancia;
                    indicador.transform.rotation = Quaternion.LookRotation(setaDirecao);

                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(seta.transform.position);
                    if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1)
                    {
                        indicador.GetComponent<Renderer>().enabled = false;
                    }
                    else
                    {
                        indicador.GetComponent<Renderer>().enabled = true;
                    }
            }   }
                else if (enemyIndicators.ContainsKey(seta))
                {
                    GameObject indicatorDeDestruicao = enemyIndicators[seta];
                    enemyIndicators.Remove(seta);
                    Destroy(indicatorDeDestruicao);

                }
            
            }

        

        List<GameObject> keysToRemove = new List<GameObject>();
        foreach(var pair in enemyIndicators)
        {
            if(pair.Key == null)
            {
                Destroy(pair.Value);
                keysToRemove.Add(pair.Key);
            }
        }
        foreach(GameObject key in keysToRemove)
        {
            enemyIndicators.Remove(key);
        }
    }

}
