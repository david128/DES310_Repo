using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FoodScript : MonoBehaviour
{
    //Sets manager
    public GameObject gameManager;
    public TextMeshProUGUI quotaFailCountText;

    public string Distributer;

    //Declare food variable
    public float food;
    float foodOver;
    public float currentFood;
    public Image foodBar;

    //Declare time variables 
    float time;
    public float[] quotaTime;
    bool timerStart = false;
    public float currentTime;
    public Image timeBar;

    //Amount required for quota
    public float[] quotaAmount;
    int currentQuota;
    int quotaCount = 0;
    bool overQuota = false;

    //money gained after food extracted
    int moneyGain;

    //money gained after food extracted
    int failToFill;

    //getter
    public float GetFood() { return currentFood; }
    public int GetCurrentQuota() { return quotaCount; }
    public float GetQuotaTimer() { return currentTime; }

    //setters
    public void SetFood(float f) { currentFood = f; }
    public void SetCurrentQuota(int q) { quotaCount = q; }
    public void SetQuotaTimer(float t) { currentTime = t; }

    //Add to current food
    public void AddFood(float f)
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("TutorialScene"))
        {   
            gameManager.GetComponent<GameLoop>().AddToTotalFood(f);
        }

        food = food + f; 
    }

    private void Start()
    {
        //DistributionChoice.instance.SetDefDistributionChoice();


        ///For testing end game screen
        //quotaCount = 9;
        //currentTime = 220;
        ///


        //Delay starting the functions
        InvokeRepeating("UpdateTimeBar", 0.0f, 0.1f);
    }

    //Update time bar
    void UpdateTimeBar()
    {
        if (timerStart == false)
        {
            timerStart = true;
        }

        timeBar.fillAmount = currentTime / quotaTime[quotaCount];
    }

    void Update()
    {
        //Updates food variables for the food bar
        if (DistributionChoice.instance.GetDistributionChoice() == "BF")
        {
            currentFood += (food * 1.5f);
            food = 0;
        }
        else if (DistributionChoice.instance.GetDistributionChoice() == "P")
        {
            currentFood += food;
            food = 0;
        }
        else if (DistributionChoice.instance.GetDistributionChoice() == "GG")
        {
            currentFood += (food * 0.5f);
            food = 0;
        }

        Distributer = DistributionChoice.instance.GetDistributionChoice();

        foodBar.fillAmount = currentFood / quotaAmount[quotaCount];

        if (currentFood > quotaAmount[quotaCount])
        {
            overQuota = true;
        }
        else
        {
            overQuota = false;
        }

        if (overQuota == false)
        {
            if (currentFood < quotaAmount[quotaCount] * 0.2f)
            {
                //Red Bar
                foodBar.color = Color.Lerp(foodBar.color, new Color(0.8773585f, 0.05067572f, 0.02069241f), Time.deltaTime * 0.8f);
            }
            else if (currentFood < quotaAmount[quotaCount] * 0.4f)
            {
                //Red-Orangey Bar
                foodBar.color = Color.Lerp(foodBar.color, new Color(0.9215686f, 0.2095846f, 0.126f), Time.deltaTime * 0.8f);
            }
            else if (currentFood < quotaAmount[quotaCount] * 0.6f)
            {
                //Orangey Bar
                foodBar.color = Color.Lerp(foodBar.color, new Color(0.895f, 0.629f, 0.14f), Time.deltaTime * 0.8f);
            }
            else if (currentFood < quotaAmount[quotaCount] * 0.8f)
            {
                //Yellowy Bar
                foodBar.color = Color.Lerp(foodBar.color, new Color(0.79f, 0.8301887f, 0.1292275f), Time.deltaTime * 0.8f);
            }
        }
        else if (overQuota == true)
        {
            //Green Bar
            //foodBar.color = Color.Lerp(foodBar.color, new Color(0.2516094f, 0.6886792f, 0.003248495f), Time.deltaTime * 0.5f);

            //Blue bar
            foodBar.color = Color.Lerp(foodBar.color, new Color(0.2509804f, 0.654902f, 0.9490197f), Time.deltaTime * 0.8f);
        }

        if (timerStart == true)
        {
            //Updates time variables for the time bar
            ///time += Time.deltaTime;
            currentTime += Time.deltaTime;

            if (currentTime >= quotaTime[quotaCount])
            {
                if (currentFood > quotaAmount[quotaCount])
                {
                    foodOver = currentFood - quotaAmount[quotaCount];

                    moneyGain = (int)foodOver;

                    if (DistributionChoice.instance.GetDistributionChoice() == "BF")
                    {
                        moneyGain = Mathf.RoundToInt(moneyGain / 10) * 15;
                    }
                    else if (DistributionChoice.instance.GetDistributionChoice() == "P")
                    {
                        moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;
                    }
                    else if (DistributionChoice.instance.GetDistributionChoice() == "GG")
                    {
                        moneyGain = Mathf.RoundToInt(moneyGain / 10) * 5;
                    }

                    moneyGain = Mathf.RoundToInt(moneyGain / 10) * 15;

                    gameManager.GetComponent<Currency>().AddMoney(moneyGain);

                    if (quotaCount != 5)
                    {
                        Debug.Log("Quota " + quotaCount + " completed");
                        quotaCount++;
                        currentQuota = quotaCount;
                    }
                    else
                    {


                        //quotaCount = 9;

                        gameManager.GetComponent<GameLoop>().FinishGame();
                    }
                }
                else if (currentFood < quotaAmount[quotaCount])
                {
                    foodOver = currentFood - quotaAmount[quotaCount];

                    moneyGain = (int)foodOver;

                    moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;

                    if (gameManager.GetComponent<Currency>().GetMoney() < Mathf.Abs(moneyGain))
                    {
                        gameManager.GetComponent<Currency>().SetMoney(0);
                    }
                    else
                    {
                        gameManager.GetComponent<Currency>().AddMoney(moneyGain);
                    }

                    //failure count
                    failToFill++;

                    //shows warning message about quota

                    quotaFailCountText.text = "" + failToFill;

                    gameManager.GetComponent<GameLoop>().GetQuotaWarning().gameObject.SetActive(true);
                    gameManager.GetComponent<InputScript>().SetAllowSelecting(false);

                    if (failToFill == 2)
                    {
                        currentQuota = quotaCount;
                        Failure();
                        failToFill = 0;
                        quotaCount = currentQuota;
                    }
                }

                currentTime = 0;
                currentFood = 0;
            }
        }
    }

    void Failure()
    {
        Debug.Log("You have failed to meet the quota too mnay times and the government have asked you to vacate your farm.");

        SceneLoader.instance.LoadEndScene(4);
        PlayerPrefs.SetInt("Ending", 0);
        //gameManager.GetComponent<Save>().LoadGameData();
    }
}