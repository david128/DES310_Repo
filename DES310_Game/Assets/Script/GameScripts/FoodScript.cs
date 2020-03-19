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
    public float maxFood = 10000;
    public float minFood = 0;
    public float currentFood;
    public Image foodBar;

    //Delcare time variables 
    float time;
    public float maxTime = 10;
    public float minTime = 0;
    public float currentTime;
    public Image timeBar;

    //Amount required for quota
    float quotaAmount = 10000;

    //money gained after food extracted
    int moneyGain;

    //getter and setter
    public float GetFood() { return food; }
    public void SetFood(float f) { food = f; }

    //Add to current food
    public void AddFood(float f) { food = food + f; }

    void Update()
    {
        //Updates time variables for the time bar
        time += Time.deltaTime;
        currentTime = time;
        timeBar.fillAmount = currentTime / maxTime;

        //Updates food variables for the food bar
        currentFood = food;
        foodBar.fillAmount = currentFood / maxFood;

        if (time >= maxTime)
        {
            if (currentFood > quotaAmount)
            {
                foodOver = currentFood - quotaAmount;

                moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;

                gameManager.GetComponent<Currency>().AddMoney(moneyGain);

            }

            if (currentFood < quotaAmount)
            {
                foodOver = quotaAmount - currentFood;

                moneyGain = Mathf.RoundToInt(moneyGain / 10) * 10;

                gameManager.GetComponent<Currency>().AddMoney(moneyGain);

            }

            time = 0;
            currentFood = 0;

            food -= food;
        }

    }

}