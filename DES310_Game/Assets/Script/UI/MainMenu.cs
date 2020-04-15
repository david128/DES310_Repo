using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject gameManager;

    public void PlayNewGame()
    {
        //loads game scene at same time as unloading the menu and sets from load to false
        SceneLoader.instance.LoadScene(1);

        //finds game manager
        gameManager = GameObject.FindWithTag("GameController");

        gameManager.GetComponent<GameLoop>().SetFromTutorial(false);
        // SceneManager.UnloadSceneAsync(0);
    }

    public void LoadSavedGame()
    {
        //loads game scene at same time as unloading the menu and sets from load to true
        SceneLoader.instance.LoadScene(2);

        //finds game manager
        gameManager = GameObject.FindWithTag("GameController");

        gameManager.GetComponent<GameLoop>().SetFromTutorial(true);
       // SceneManager.UnloadSceneAsync(0);
    }

    public void BackToMenu()
    {
        //loads menu scene at same time as unloading the game and sets from load to false
        SceneLoader.instance.LoadScene(0);
        //SceneManager.UnloadSceneAsync(1);
    }

    public void QuitGame()
    {
        //Quits application
        Debug.Log("Quit");

        Application.Quit();
    }
}
