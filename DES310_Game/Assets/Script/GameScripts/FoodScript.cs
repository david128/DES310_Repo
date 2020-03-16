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
    public float maxFood = 10000;
    public float minFood = 0;
    public float currentFood;
    public Image foodBar;

    //Delcare time variables
    public float maxTime = 10;
    public float minTime = 0;
    public float currentTime;
    public Image timeBar;

    float time;

    //Amount required for quota
    float quotaAmount = 10000;

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
            time = 0;
            currentFood = 0;

            if (food > quotaAmount)
            {
                float foodOver;
                int moneyGain;

                foodOver = quotaAmount - currentFood;

            }

            food -= food;
        }

    }

}