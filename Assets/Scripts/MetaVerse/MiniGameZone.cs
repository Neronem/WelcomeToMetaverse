using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameZone : MonoBehaviour
{
    private bool isPlayerInZone = false;
    public GameObject EventUI;

    private void Awake()
    {
        EventUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            ChangeScene.instance.FadeToScene("MiniGame");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            Debug.Log("Player entered");
        }

        if (EventUI != null)
        {
            EventUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            Debug.Log("Player exited");
        }
        
        if(EventUI != null){
            EventUI.SetActive(false);
        }
    }
}
