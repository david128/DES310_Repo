using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public bool fromLoad;

    public bool GetFromLoad() { return fromLoad; }

    void Start()
    {
        instance = this;
    }

    public void PlayGame()
    {
        //loads game scene at same time as unloading the menu and sets from load to false
        SceneManager.LoadSceneAsync(1);
        fromLoad = false;
       // SceneManager.UnloadSceneAsync(0);
    }

    public void LoadSavedGame()
    {
        //loads game scene at same time as unloading the menu and sets from load to true
        SceneManager.LoadSceneAsync(1);
        fromLoad = true;
       // SceneManager.UnloadSceneAsync(0);
    }

    public void BackToMenu()
    {
        //loads menu scene at same time as unloading the game and sets from load to false
        SceneManager.LoadSceneAsync(0);
        //SceneManager.UnloadSceneAsync(1);
    }

    public void QuitGame()
    {
        //Quits application
        Debug.Log("Quit");

        Application.Quit();
    }
  
}
