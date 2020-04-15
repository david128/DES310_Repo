using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject NewGameConfirm;
    public GameObject NoSaveNotice;

    public void CheckNewGame()
    {
        string path = Application.persistentDataPath + "/saveData.SaveData";

        //loads game scene at same time as unloading the menu and sets from load to false

        if (File.Exists(path))
        {
            NewGameConfirm.SetActive(true);
        }
        else
        {
            SceneLoader.instance.LoadScene(1);
            PlayerPrefs.SetInt("loadGame", 0);
        }

        // SceneManager.UnloadSceneAsync(0);
    }

    public void PlayNewGame()
    {
        SceneLoader.instance.LoadScene(1);
        PlayerPrefs.SetInt("loadGame", 0);
    }

    public void LoadSavedGame()
    {
        string path = Application.persistentDataPath + "/saveData.SaveData";

        //loads game scene at same time as unloading the menu and sets from load to true
        if (File.Exists(path))
        {
            SceneLoader.instance.LoadScene(2);
            PlayerPrefs.SetInt("loadGame", 1);
        }
        else
        {
            NoSaveNotice.SetActive(true);
        }

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
