using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Time.timeScale = 1f;
            RestartCurrentScene();
        }



        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.BackToMenu();
        }




    }

    public void RestartCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}