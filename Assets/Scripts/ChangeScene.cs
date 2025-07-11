using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    public Image image;
    public float fadeDuration;

    private bool isFirstScene = true;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject fadeInCanvas = image.transform.root.gameObject;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(fadeInCanvas.gameObject);
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (isFirstScene)
        {
            isFirstScene = false;
        }
        else
        {
            if (image == null)
            {
                image = FindObjectOfType<Image>();
            }
            StartCoroutine(FadeIn());
        }
    }
    
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
    
    private IEnumerator FadeOut(string sceneName)
    {
        image.gameObject.SetActive(true);        
        float timeElapsed = 0;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            image.color = new Color(0, 0, 0, alpha);
            yield return null; // Update랑 비슷하게 한프레임씩 실행되게해줌
        }

        image.color = Color.black;
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator FadeIn()
    {
        float timeElapsed = 0f;
        
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            image.color = new Color(0, 0, 0, alpha); 
            yield return null; 
        }

        image.color = new Color(0, 0, 0, 0); 
        
        image.gameObject.SetActive(false);
    }
}
