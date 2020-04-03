﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    //Declare variables
    public GameObject gameManager, textManager;

    public float time;
    public float FPS;

    //Frames per second
    public float GetFPS() { return FPS; }

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
        
        //load events
        gameManager.GetComponent<Events>().HandleEventFile();

        //checks if the tutorial scene i
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialScene"))
        {
            //Creates grid at the start
            gameManager.GetComponent<GridScript>().CreateGrid(true);
        }
        else
        {
            gameManager.GetComponent<GridScript>().CreateGrid(false);
        }

        ////Checks if the player is trying to load their saved game
        //if (MainMenu.instance.GetFromLoad() == true)
        //{
        //    gameManager.GetComponent<Save>().LoadGameData();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(QualitySettings.vSyncCount.ToString());
        //Adds up time passed every frame
        time += Time.deltaTime;

        FPS = 1.0f / Time.deltaTime;

        //When the time gets to 3 seconds the money will increase causing a passive income
        if (time > 3)
        {
            //Resets the period of time for the passive income
            time = 0.0f;

            //checks for events
            gameManager.GetComponent<Events>().checkTrigger();
        }

        //Gets input from player
        gameManager.GetComponent<InputScript>().GetInput();

        //Updates the UI text
        textManager.GetComponent<TextScript>().UpdateText();
    }

    // Quits the player when the user hits escape
    public void QuitGame()
    {
        //Saves Game
        ///Done in button now
        //saveGame.SaveGameData();

        Debug.Log("Quit Application");

        //Quits application
        Application.Quit();

       
    }
}
