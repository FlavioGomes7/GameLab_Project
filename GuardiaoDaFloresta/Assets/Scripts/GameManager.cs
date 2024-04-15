using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI pointsText;
    public string enemyTag = "Enemy"; 
    public TextMeshProUGUI enemyCountText; 
    public string treeTag = "tree";
    public TextMeshProUGUI treeCountText;
    
    private float timer = 0f;
    private int enemyCount = 0;
    public GameObject winCanvas;
    public GameObject loseCanvas;


    [SerializeField] private PlayerScriptableObject stats;
    public static GameManager instance;

    public void Awake()
    {
        instance = this;
    }

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
        pointsText.text = "Pontos: " + stats.moneyPlayer;
        GameObject[] enemiesWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemiesWithTag.Length;
        enemyCountText.text = "Inimigos: " + enemyCount.ToString();
        GameObject[] treesWithTag = GameObject.FindGameObjectsWithTag("Tree");
        enemyCount = treesWithTag.Length;
        treeCountText.text = "Árvores: " + enemyCount.ToString();


        if (treesWithTag.Length <= 0)
        {
            Time.timeScale = 0;
            loseCanvas.SetActive(true);
        }

    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

   public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

   public void AddMoney(float value)
   {
        stats.moneyPlayer += value;
   }
}
