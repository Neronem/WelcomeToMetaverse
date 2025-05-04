using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static  UIManager instance;
    
    public GameObject startUI;

    private Player player;
    public GameObject scoreUI;
    public TextMeshProUGUI scoreText;
    
    private GameObject gameOverUI;
    private GameOverUI gameOverUIScript;
    
    private bool isGameStarted = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        player = FindObjectOfType<Player>();
        gameOverUI = GameObject.Find("GameOverUI");
        gameOverUIScript = GameObject.Find("GameOverUI").GetComponent<GameOverUI>();
        
        startUI.SetActive(false);
        if (!GameManager.isGameRestarted)
        {
            startUI.SetActive(true);
        }
        scoreUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameRestarted)
        {
            StartGame();
            GameManager.isGameRestarted = false;
        }
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
        
        
        float walked = player.playerWalked;
        scoreText.text = "간 거리 : " + walked.ToString("F1") + "m";
    }

    void StartGame()
    {
        isGameStarted = true;
        startUI.SetActive(false);
        Time.timeScale = 1f;
        scoreUI.SetActive(true);
    }

    public void GameOverUIAppear()
    {
        gameOverUI.SetActive(true);
        gameOverUIScript.SetUI(player.playerWalked, player.bestPlayerWalked);
    }

    public void GameOverUIDisappear()
    {
        gameOverUI.SetActive(false);
    }
}
