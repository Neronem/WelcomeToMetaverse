using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    private float bestScoreValue;
    
    private void Start()
    {
        gameObject.SetActive(false);
        bestScoreValue = PlayerPrefs.GetFloat("BestScore", 0);
    }

    public void ShowLeaderBoard()
    {
        gameObject.SetActive(true);
        if (bestScoreValue == 0)
        {
            bestScore.text = "기록 없음!";
        }
        else
        {
            bestScore.text = bestScoreValue.ToString("F1");
        }
    }

    public void HideLeaderBoard()
    {
        gameObject.SetActive(false);
    }
}
