using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    public void SetUI(float score, float bestScore)
    {
        currentScoreText.text = score.ToString("F1");
        bestScoreText.text = bestScore.ToString("F1");
    }
}
