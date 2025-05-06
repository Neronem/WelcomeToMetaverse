using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSelect : MonoBehaviour
{
    public static PlayerSkinSelect instance;
    
    public GameObject[] characterSprites;
    public GameObject characterSelectUI;
    
    public bool isPlayerSelected = false;
    public GameObject selectedCharacter;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!isPlayerSelected)
        {
            foreach (GameObject characterSprite in characterSprites)
            {
                characterSprite.SetActive(false);
            }

            characterSelectUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SelectCharacter(int index)
    {
        selectedCharacter = characterSprites[index];
        selectedCharacter.SetActive(true);
        characterSelectUI.SetActive(false);
            
        isPlayerSelected = true;
        Time.timeScale = 1;
                
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.Init();
    }
}
