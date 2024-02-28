using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    [SerializeField] private PlayerScriptableObject scriptableObject;

    private void Start()
    {
        scriptableObject.ResetStatus();
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

    public void Quit()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }
}
