using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentScore = 0;
    public static bool isGameRestarted = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOver()
    {
        
    }

    public void ReStartGame()
    {
        isGameRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }
}
