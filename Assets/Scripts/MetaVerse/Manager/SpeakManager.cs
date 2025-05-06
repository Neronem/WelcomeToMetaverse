using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeakManager : MonoBehaviour
{
    public static SpeakManager instance;
    
    private GameObject currentUI;
    public PlayerController playerController;
    
    public TextMeshProUGUI dialogText; // 대화 텍스트
    public List<string> dialogLines; // 대화 텍스트 모음집
    private int currentLine = 0; // 현재 라인

    private bool isTaking = false; // 대화하고있는지 여부 체크

    public AudioClip dialogSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isTaking && Input.GetKeyDown(KeyCode.Return)) // 대화중 Enter입력시 다음대화로 넘어감
        {
            ShowNextLine();
        }
    }

    public void StartSpeak(List<string> Lines, GameObject npcUI, TextMeshProUGUI Text)
    {
        currentUI = npcUI;
        playerController.StopMovement();
        currentUI.SetActive(true);
        dialogText = Text;
        dialogLines = Lines;
        
        currentLine = 0;
        isTaking = true;

        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLine < dialogLines.Count)
        {
            dialogText.text = dialogLines[currentLine];
            currentLine++;

            if (dialogSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(dialogSFX);
            }
        }
        else
        {
            EndSpeak();
        }
    }

    void EndSpeak()
    {
        isTaking = false;
        playerController.StartMovement();
        currentUI.SetActive(false);
    }
}
