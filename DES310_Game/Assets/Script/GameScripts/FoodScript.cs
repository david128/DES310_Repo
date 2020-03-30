using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodScript : MonoBehaviour
{
    //Sets manager
    public GameObject gameManager;

    //Declare food variable
    public float food;
    float foodOver;
    public float currentFood;
    public Image foodBar;

    //Delcare time variables 
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

    //getter and setter
    public float GetFood() { return food; }
    public void SetFood(float f) { food = f; }

    //Add to current food
    public void AddFood(float f) { food = food + f; }

    private void Start()
    {
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
        currentFood = food;
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
            time += Time.deltaTime;
            currentTime = time;

            if (currentTime >= quotaTime[quotaCount])
            {
                if (currentFood > quotaAmount[quotaCount])
                {
                    foodOver = currentFood - quotaAmount[quotaCount];

                    moneyGain = (int)foodOver;

                    moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;

                    gameManager.GetComponent<Currency>().AddMoney(moneyGain);

                    if (quotaCount != 9)
                    {
                        Debug.Log("Quota " + quotaCount + " completed");
                        quotaCount++;
                        currentQuota = quotaCount;
                        
                    }
                    else
                    {
                        if (gameManager.GetComponent<SustainabilityScript>().GetSustainability() > 10)
                        {
                            Debug.Log("You have completed the game with a good ending");
                        }
                        else
                        {
                            Debug.Log("You have completed the game with a bad ending");
                        }

                        quotaCount = 9;
                    }
                }
                else if (currentFood < quotaAmount[quotaCount])
                {
                    foodOver = currentFood - quotaAmount[quotaCount];

                    moneyGain = (int)foodOver;

                    moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;

                    gameManager.GetComponent<Currency>().AddMoney(moneyGain);

                    //failure count
                    failToFill++;

                    if (failToFill == 5)
                    {
                        currentQuota = quotaCount;
                        Failure();
                        failToFill = 0;
                        quotaCount = currentQuota;
                    }
                }

                time = 0;
                currentFood = 0;

                food -= food;
            }
        }
    }

    void Failure()
    {
        Debug.Log("You have failed to meet the quota too mnay times and the government have asked you to vacate your farm.");
        //gameManager.GetComponent<Save>().LoadGameData();
    }

}