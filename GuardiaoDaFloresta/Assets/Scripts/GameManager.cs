using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; 
    public string enemyTag = "Enemy"; 
    public TextMeshProUGUI enemyCountText; 
    public string treeTag = "tree";
    public TextMeshProUGUI treeCountText;
    
    private float timer = 0f;
    private int objectCount = 0;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    void Update()
    {

        timer += Time.deltaTime;
        UpdateTimeText();


        CountObjectsWithTag();

        


    }

    void UpdateTimeText()
    {
        // Atualiza o texto do tempo
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timeText.text = string.Format("Tempo: {0:00}:{1:00}", minutes, seconds);
    }

    void CountObjectsWithTag()
    {
        
        GameObject[] enemiesWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        objectCount = enemiesWithTag.Length;
        enemyCountText.text = "Inimigos: " + objectCount.ToString();
        GameObject[] treesWithTag = GameObject.FindGameObjectsWithTag("Tree");
        objectCount = treesWithTag.Length;
        treeCountText.text = "Árvores: " + objectCount.ToString();


        if (treesWithTag.Length <= 0)
        {
            Time.timeScale = 0;
            loseCanvas.SetActive(true);
        }


    }
}
