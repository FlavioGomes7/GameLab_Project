using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private Image[] winStars;
    [SerializeField] private Image[] loseStars;
    private bool wasWinned = false;
    private bool wasHitted = false;
    private bool wasDied = false;

    public UnityEvent winned;
    public UnityEvent hitted;
    public UnityEvent died;
    public UnityEvent showScore;

    public void WasWinned()
    {
        if(!wasWinned)
        {
            wasWinned = true;
            score += 250;
        }
    }

    public void WasHitted()
    {
        if (!wasHitted)
        {
            wasHitted = true;
            score -= 500;
        }  
    }

    public void WasDied()
    {
        if(!wasDied)
        {
            wasDied = true;
            score -= 250;
        }
    }

    public void ShowScore()
    {
        if (score < 250)
        {
            Debug.Log("0 Star");
            Debug.Log(score);
        }
        else if (score >= 250 && score < 500)
        {
            Debug.Log("1 Star");
            winStars[0].gameObject.SetActive(true);
            loseStars[0].gameObject.SetActive(true);
            Debug.Log(score);
        }
        else if (score >= 500 && score < 1000)
        {
            Debug.Log("2 Star");
            winStars[0].gameObject.SetActive(true);
            loseStars[0].gameObject.SetActive(true);
            winStars[1].gameObject.SetActive(true);
            loseStars[1].gameObject.SetActive(true);
            Debug.Log(score);
        }
        else
        {
            Debug.Log("3 Star");
            winStars[0].gameObject.SetActive(true);
            loseStars[0].gameObject.SetActive(true);
            winStars[1].gameObject.SetActive(true);
            loseStars[1].gameObject.SetActive(true);
            winStars[2].gameObject.SetActive(true);
            loseStars[2].gameObject.SetActive(true);
            Debug.Log(score);
        }
    }

    public void Start()
    {
        winStars[0].gameObject.SetActive(false);
        loseStars[0].gameObject.SetActive(false);
        winStars[1].gameObject.SetActive(false);
        loseStars[1].gameObject.SetActive(false);
        winStars[2].gameObject.SetActive(false);
        loseStars[2].gameObject.SetActive(false);
    }
}
