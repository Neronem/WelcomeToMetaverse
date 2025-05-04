using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnShowUI : MonoBehaviour
{
    private float bestScore = 0f;
    public GameObject UIOfReturn;
    
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI TalkAboutYou;
    
    private bool TurnUION = false;
    private static bool isFirstCamed = true;
    
    // Start is called before the first frame update
    void Start()
    {
        UIOfReturn.SetActive(false);

        if (!isFirstCamed)
        {
            bestScore = PlayerPrefs.GetFloat("BestScore", 0);
            if (bestScore != 0)
            {
                TurnUION = true;
                BestScoreText.text = bestScore.ToString("F1");
                if (bestScore < 200f)
                {
                    TalkAboutYou.text = "200점정도면 꽤 멋있을거에요.";
                }
                else if (bestScore >= 200f)
                {
                    TalkAboutYou.text = "꽤 실력자군요.";
                }

                UIOfReturn.SetActive(true);
            }
        }
        else
        {
            isFirstCamed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && TurnUION)
        {
            UIOfReturn.SetActive(false);
            TurnUION = false;
        }
    }
}
