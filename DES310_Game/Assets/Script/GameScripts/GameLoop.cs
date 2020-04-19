using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    //Declare variables
    public GameObject gameManager, textManager;

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

    //getters
    public Image GetUpgradeWarning() { return UpgradeWarning; }
    public Image GetMoneyWarning() { return MoneyWarning; }
    public Image GetQuotaWarning() { return QuotaWarning; }
    
    //Total "" getters
    public int GetTotalMoneyEarned() { return totalMoneyEarned; }
    public int GetTotalMoneySpent() { return totalMoneySpent; }
    public float GetTotalFood() { return totalFood; }

    //setters
    public void SetTotalMoneyEarned(int tM) { totalMoneyEarned = tM; }
    public void SetTotalMoneySpent(int tM) { totalMoneySpent = tM; }
    public void SetTotalFood(float tF) { totalFood = tF; }

    public void AddToTotalMoneyEarned(int tM) { totalMoneyEarned += tM; }
    public void AddToTotalMoneySpent(int tM) { totalMoneySpent += tM; }
    public void AddToTotalFood(float tF) { totalFood += tF; }

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
        if (time > 60)
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

    // Quits the player when the user hits escape
    public void FinishGame()
    {
        //gets all stats info
        //GatherStats();

        SceneLoader.instance.LoadEndScene(4);
        PlayerPrefs.SetInt("Ending", 1);
    }

    // looks for and finds end game stats to show player
    public void GatherStats()
    {
        //gets all stats info

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
