using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPController : MonoBehaviour
{
    public GameObject NPCUI;
    public TextMeshProUGUI dialogText;
    
    public bool hasSpoken = false;

    private void Start()
    {
        NPCUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasSpoken)
        {
        List<string> lines = new List<string>
        {
            "...."
        };
            
        SpeakManager.instance.StartSpeak(lines, NPCUI, dialogText);
        }
        
        if (other.CompareTag("Player") && !hasSpoken)
        {
            hasSpoken = true;
            
            List<string> lines = new List<string>
            {
                "안녕하세용",
                "저는 그냥 장식이에용",
                "그런데 말을할수있는 이유는"
            };
            
            SpeakManager.instance.StartSpeak(lines, NPCUI, dialogText);
        }

    }
}
