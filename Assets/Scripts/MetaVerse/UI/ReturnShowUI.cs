using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnShowUI : MonoBehaviour
{
    private float bestScore = 0f;
    public GameObject UIOfReturn;
    
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI TalkAboutYOu;

    private bool isPlayedBefore = false;
    
    // Start is called before the first frame update
    void Start()
    {
        UIOfReturn.SetActive(false);
        
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        if (bestScore != 0)
        {
            isPlayedBefore = true;
            BestScoreText.text = bestScore.ToString("F1");
            if (bestScore < 200f)
            {
                TalkAboutYOu.text = "200점정도면 꽤 멋있을거에요.";
            }
            else if (bestScore >= 200f)
            {
                TalkAboutYOu.text = "꽤 실력자군요.";
            }
            UIOfReturn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayedBefore)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIOfReturn.SetActive(false);
                isPlayedBefore = false;
            }
        }
    }
}
