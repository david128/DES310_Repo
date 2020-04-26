﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    //Declare variables
    public GameObject gameManager, textManager;
   
    public AK.Wwise.Bank MyBank = null;
    public AK.Wwise.Event MyEvent = null;

    //Warnings
    public Image UpgradeWarning;
    public Image MoneyWarning;
    public Image QuotaWarning;

    public float time;
    public float FPS;

    //Seasoon varaibles
    public string season;
    public float seasonTimer;
    bool changingSeason;

    //Season colours
    Color spring;
    Color sprTree;
    Color sprLight;

    Color summer;
    Color sumTree;
    Color sumLight;

    Color autumn;
    Color autTree;
    Color autLight;

    Color winter;
    Color winTree;
    Color winLight;

    public Material GrassMat;
    public Material TreeMat;
    public Light lighting;

    //Total variables for stats menus
    public int totalMoneyEarned;
    public int totalMoneySpent;
    public float totalFood;
    public float totalTimePlayed;
    public int totalPeopleFed;

    //SelectedTile vairable
    public GameObject selectedTile;

    //getters
    public Image GetUpgradeWarning() { return UpgradeWarning; }
    public Image GetMoneyWarning() { return MoneyWarning; }
    public Image GetQuotaWarning() { return QuotaWarning; }
    
    //Total "" getters
    public int GetTotalMoneyEarned() { return totalMoneyEarned; }
    public int GetTotalMoneySpent() { return totalMoneySpent; }
    public float GetTotalFood() { return totalFood; }
    public float GetTotalTimePlayed() { return totalTimePlayed; }
    public int GetTotalPeopleFed() { return totalPeopleFed; }

    //Selected Tile getter
    public GameObject GetSelectedTile() { return selectedTile; }

    //setters
    public void SetTotalMoneyEarned(int tM) { totalMoneyEarned = tM; }
    public void SetTotalMoneySpent(int tM) { totalMoneySpent = tM; }
    public void SetTotalFood(float tF) { totalFood = tF; }
    public void SetTotalTimePlayed(float tP) { totalTimePlayed = tP; }
    public void SetTotalPeopleFed(int pF) { totalPeopleFed = pF; }

    //Add to total amount
    public void AddToTotalMoneyEarned(int tM) { totalMoneyEarned += tM; }
    public void AddToTotalMoneySpent(int tM) { totalMoneySpent += tM; }
    public void AddToTotalFood(float tF) { totalFood += tF; }
    public void AddToTotalTimePlayed(float tP) { totalTimePlayed += tP; }
    public void AddToTotalPeopleFed(int pF) { totalPeopleFed += pF; }

    //sets selected tile
    public void SetSelectedTile(GameObject s) { selectedTile = s; }

    //Frames per second
    public float GetFPS() { return FPS; }

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
        
        //load events
        gameManager.GetComponent<Events>().HandleEventFile();

        //Set season colours
        spring = new Color(0.1685667f, 0.4056604f, 0.03252938f);
        sprTree = new Color(0.530616f, 0.7075472f, 0.03671236f);
        sprLight = new Color(0.949f, 0.6919309f, 0.0f);

        summer = new Color(0.0352142f, 0.4304226f, 0.0f);
        sumTree = new Color(0.1496457f, 0.284f, 0.10934f);
        sumLight = new Color(0.9757641f, 1.0f, 0.7568628f);

        autumn = new Color(0.147f, 0.189f, 0.0f);
        autTree = new Color(0.4245283f, 0.1979121f, 0.0f);
        autLight = new Color(1.0f, 0.7317073f, 0.629f);

        winter = new Color(0.2569865f, 0.5188679f, 0.3934735f);
        winTree = new Color(0.259f, 0.35f, 0.462f);
        winLight = new Color(0.6078432f, 0.8511306f, 1.0f);

        //Change material colours
        GrassMat.color = spring;
        TreeMat.color = sprTree;
        lighting.color = sprLight;

        season = "spring";

        //checks if the tutorial scene i
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialScene"))
        {
            //deletes old save data
            gameManager.GetComponent<Save>().DeleteGameData();

            //Creates grid at the start
            gameManager.GetComponent<GridScript>().CreateGrid(true);
        }
        else
        {
            gameManager.GetComponent<GridScript>().CreateGrid(false);

        }

        if(PlayerPrefs.GetInt("loadGame") == 1)
        {
            gameManager.GetComponent<Save>().LoadGameData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(QualitySettings.vSyncCount.ToString());
        //Adds up time passed every frame
        time += Time.deltaTime;

        seasonTimer += Time.deltaTime;

        FPS = 1.0f / Time.deltaTime;

        //When the time gets to 3 seconds the money will increase causing a passive income
        if (time > 20)
        {
            //Resets the period of time for the passive income
            time = 0.0f;

            //checks for events
            gameManager.GetComponent<Events>().checkTrigger();
        }


        if (changingSeason == false)
        {
            if (seasonTimer < 46.0f && season != "spring")
            {
                season = "spring";
                StartCoroutine(ChangeSeason());
            }
            else if (seasonTimer > 46.0f && seasonTimer < 96.0f && season != "summer")
            {
                season = "summer";
                StartCoroutine(ChangeSeason());
            }
            else if (seasonTimer > 96.0f && seasonTimer < 150.0f && season != "autumn")
            {
                season = "autumn";
                StartCoroutine(ChangeSeason());
            }
            else if (seasonTimer > 150.0f && seasonTimer < 193.0f && season != "winter")
            {
                season = "winter";
                StartCoroutine(ChangeSeason());
            }
            else if (seasonTimer > 200.0f)
            {
                seasonTimer = 0;
            }
        }

        //Gets input from player
        gameManager.GetComponent<InputScript>().GetInput();

        //Updates the UI text
        textManager.GetComponent<TextScript>().UpdateText();
    }

    IEnumerator ChangeSeason()
    {
        bool done = false;

        float slowerChangeTime = 0;
        float fasterChangeTime = 0;
        float superSlowChangeTime = 0;

        changingSeason = true;

        while (!done)
        {
            slowerChangeTime += Time.deltaTime * 0.003f;
            fasterChangeTime += Time.deltaTime * 0.005f;
            superSlowChangeTime += Time.deltaTime * 0.0005f;

            if (season == "spring")
            {
                //White-green tone
                GrassMat.color = Color.Lerp(GrassMat.color, spring, slowerChangeTime);
                TreeMat.color = Color.Lerp(TreeMat.color, sprTree, superSlowChangeTime);
                lighting.color = Color.Lerp(lighting.color, sprLight, slowerChangeTime);

                if (GrassMat.color == spring && lighting.color == sprLight)
                {
                    done = true;
                }

            }
            else if (season == "summer")
            {
                //Green-yellow tone
                GrassMat.color = Color.Lerp(GrassMat.color, summer, fasterChangeTime);
                TreeMat.color = Color.Lerp(TreeMat.color, sumTree, superSlowChangeTime);
                lighting.color = Color.Lerp(lighting.color, sumLight, slowerChangeTime);

                if (GrassMat.color == summer && lighting.color == sumLight)
                {
                    done = true;
                }
            }
            else if (season == "autumn")
            {
                //Brown-Orangey tone
                GrassMat.color = Color.Lerp(GrassMat.color, autumn, fasterChangeTime);
                TreeMat.color = Color.Lerp(TreeMat.color, autTree, superSlowChangeTime);
                lighting.color = Color.Lerp(lighting.color, autLight, slowerChangeTime);

                if (GrassMat.color == autumn && lighting.color == autLight)
                {
                    done = true;
                }
            }
            else if (season == "winter")
            {
                //Blue-green tone
                GrassMat.color = Color.Lerp(GrassMat.color, winter, fasterChangeTime);
                TreeMat.color = Color.Lerp(TreeMat.color, winTree, superSlowChangeTime);
                lighting.color = Color.Lerp(lighting.color, winLight, slowerChangeTime);

                if (GrassMat.color == winter && lighting.color == winLight)
                {
                    done = true;
                }
            }

            yield return null;
        }

        changingSeason = false;
    }

    public void SaveGameStats(int moneyEarned, int moneySpent, float foodProduced, float timePlayed, int peopleFed)
    {
        PlayerPrefs.SetInt("TotalMoneyEarned", moneyEarned);
        PlayerPrefs.SetInt("TotalMoneySpent", moneySpent);
        PlayerPrefs.SetFloat("TotalFoodProduced", foodProduced);
        PlayerPrefs.SetFloat("TotalTimePlayed", timePlayed);
        PlayerPrefs.SetInt("PeopleFed", peopleFed);
    }

    public void SaveFieldStats(ObjectInfo.ObjectType type, ObjectFill.FillType fill, int tileCount, int moneyCount , int foodCount)
    {
        switch (type)
        {
            case ObjectInfo.ObjectType.FIELD:

                switch (fill)
                {
                    case ObjectFill.FillType.WHEAT:
                        PlayerPrefs.SetInt("WheatFieldCount", tileCount);
                        PlayerPrefs.SetInt("WheatFieldFood", moneyCount);
                        PlayerPrefs.SetInt("WheatFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.CORN:
                        PlayerPrefs.SetInt("CornFieldCount", tileCount);
                        PlayerPrefs.SetInt("CornFieldFood", moneyCount);
                        PlayerPrefs.SetInt("CornFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.CARROT:
                        PlayerPrefs.SetInt("CarrotFieldCount", tileCount);
                        PlayerPrefs.SetInt("CarrotFieldFood", moneyCount);
                        PlayerPrefs.SetInt("CarrotFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.POTATO:
                        PlayerPrefs.SetInt("CabbageFieldCount", tileCount);
                        PlayerPrefs.SetInt("CabbageFieldFood", moneyCount);
                        PlayerPrefs.SetInt("CabbageFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.TURNIP:
                        PlayerPrefs.SetInt("TurnipFieldCount", tileCount);
                        PlayerPrefs.SetInt("TurnipFieldFood", moneyCount);
                        PlayerPrefs.SetInt("TurnipFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.SUGARCANE:
                        PlayerPrefs.SetInt("SugarFieldCount", tileCount);
                        PlayerPrefs.SetInt("SugarFieldFood", moneyCount);
                        PlayerPrefs.SetInt("SugarFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.SUNFLOWER:
                        PlayerPrefs.SetInt("SunflowerFieldCount", tileCount);
                        PlayerPrefs.SetInt("SunflowerFieldFood", moneyCount);
                        PlayerPrefs.SetInt("SunflowerFieldMoney", foodCount);
                        break;

                    case ObjectFill.FillType.COCCOA:
                        PlayerPrefs.SetInt("CocoaFieldCount", tileCount);
                        PlayerPrefs.SetInt("CocoaFieldFood", moneyCount);
                        PlayerPrefs.SetInt("CocoaFieldMoney", foodCount);
                        break;
                }

                break;

            case ObjectInfo.ObjectType.RICE:
                PlayerPrefs.SetInt("RiceFieldCount", tileCount);
                PlayerPrefs.SetInt("RiceFieldFood", moneyCount);
                PlayerPrefs.SetInt("RiceFieldMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.COW_FIELD:
                PlayerPrefs.SetInt("CowFieldCount", tileCount);
                PlayerPrefs.SetInt("CowFieldFood", moneyCount);
                PlayerPrefs.SetInt("CowFieldMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                PlayerPrefs.SetInt("ChickenFieldCount", tileCount);
                PlayerPrefs.SetInt("ChickenFieldFood", moneyCount);
                PlayerPrefs.SetInt("ChickenFieldMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.PIG_PEN:
                PlayerPrefs.SetInt("PigFieldCount", tileCount);
                PlayerPrefs.SetInt("PigFieldFood", moneyCount);
                PlayerPrefs.SetInt("PigFieldMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.BATTERY:
                PlayerPrefs.SetInt("BatteryCount", tileCount);
                PlayerPrefs.SetInt("BatteryFood", moneyCount);
                PlayerPrefs.SetInt("BatterybMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.MEAT_LAB:
                PlayerPrefs.SetInt("MeatLabCount", tileCount);
                PlayerPrefs.SetInt("MeatLabFood", moneyCount);
                PlayerPrefs.SetInt("MeatLabMoney", foodCount);
                break;

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                PlayerPrefs.SetInt("HydroCount", tileCount);
                PlayerPrefs.SetInt("HydroFieldFood", moneyCount);
                PlayerPrefs.SetInt("HydroFieldMoney", foodCount);
                break;

            default:
                return;
        }
    }

    // Quits the player when the user hits escape
    public void FinishGame()
    {
        //gets all stats info
        //GatherStats();

        SceneLoader.instance.LoadEndScene(4);
        PlayerPrefs.SetInt("Ending", 1);
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
