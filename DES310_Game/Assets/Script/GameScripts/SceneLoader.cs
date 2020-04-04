﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    //Loading screen variables
    public GameObject loadingScreen;
    public Slider loadSlider;
    public TextMeshProUGUI progressText;

    //Static nstance of the class
    public static SceneLoader instance;

    void Start()
    {
        instance = this;
    }

    //Starts loading scene bar 
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadSlider.value = progress;
            progressText.text = progress * 100 + "%";

            Debug.Log(progress);

            yield return null;
        }
    }

}
