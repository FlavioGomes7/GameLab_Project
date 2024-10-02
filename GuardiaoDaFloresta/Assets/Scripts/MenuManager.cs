using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    [SerializeField] private PlayerScriptableObject scriptableObject;

    [SerializeField] private TextMeshProUGUI pointsText;

    private void Start()
    {
        scriptableObject.ResetStatus();
        scriptableObject.isReset = true;
    }

    private void Update()
    {
        pointsText.text = "Pontos: " + scriptableObject.moneyPlayer;
    }

    public void GoToTitleMenu()
    {
        foreach(GameObject ob in menus)
        {
            ob.SetActive(false);
        }
        menus[0].SetActive(true);
    }
    
    public void GoToOptions()
    {
        foreach(GameObject ob in menus)
        {
            ob.SetActive(false);
        }
        menus[1].SetActive(true);
    }

     public void GoToLevelSelector()
    {
        foreach(GameObject ob in menus)
        {
            ob.SetActive(false);
        }
        menus[2].SetActive(true);
    }

    public void GoToShop()
    {
        foreach (GameObject ob in menus)
        {
            ob.SetActive(false);
        }
        menus[3].SetActive(true);
    }

    public void OpenLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        scriptableObject.isReset = false;
        Debug.Log("Saiu");
        Application.Quit();
    }
    
 
}
