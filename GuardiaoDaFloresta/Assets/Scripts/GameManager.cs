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
    [SerializeField] private GameObject spawner;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    public static GameManager instance;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerScriptableObject stats;
    [SerializeField] private ScoreSystem scoreSystem;

    public void Awake()
    {
        instance = this;
        Time.timeScale = 1;
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
        pointsText.text = "Pontos: " + playerManager.pointsCurrent;
        GameObject[] enemiesWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemiesWithTag.Length;
        enemyCountText.text = "Inimigos: " + enemyCount.ToString();
        GameObject[] treesWithTag = GameObject.FindGameObjectsWithTag("Tree");
        enemyCount = treesWithTag.Length;
        treeCountText.text = "Árvores: " + enemyCount.ToString();


        if (treesWithTag.Length <= 0)
        {
            scoreSystem.showScore.Invoke();
            loseCanvas.SetActive(true);
        }

    }

    public void WinLevel()
    {
        scoreSystem.winned.Invoke();
        scoreSystem.showScore.Invoke();
        winCanvas.SetActive(true);
        spawner.SetActive(false);
    }

   public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

   public void OnDeath(float points)
   {
        playerManager.AddPoints(points);
   }
}
