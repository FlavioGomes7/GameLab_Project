using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;
    private bool wasWinned = false;
    private bool wasHitted = false;
    private bool wasDied = false;

    public UnityEvent winned;
    public UnityEvent hitted;
    public UnityEvent died;
    public UnityEvent showScore;

    public void WasWinned()
    {
        wasWinned = true;
        score += 250;
        Debug.Log(wasWinned);
    }

    public void WasHitted()
    {
        wasHitted = true;
        score -= 500;
        Debug.Log(wasHitted);
    }

    public void WasDied()
    {
        wasDied = true;
        score -= 250;
        Debug.Log(wasDied);
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
            Debug.Log(score);
        }
        else if (score >= 500 && score < 1000)
        {
            Debug.Log("2 Star");
            Debug.Log(score);
        }
        else
        {
            Debug.Log("3 Star");
            Debug.Log(score);
        }
    }

}
