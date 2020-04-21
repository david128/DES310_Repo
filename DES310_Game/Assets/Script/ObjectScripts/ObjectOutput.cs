using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutput : MonoBehaviour
{
    GameObject gameManager;

    public bool AttatchedToFill;
    public int[] moneyOutput;
    public int[] foodOutput;
    int level;

    //Getters
    public int[] GetTileMoneyOutput() { return moneyOutput; }
    public int[] GetTileFoodOutput() { return foodOutput; }

    void Start()
    {

        //if attatched to the fill then need to get from parent
        if (AttatchedToFill)
        {
            level = GetComponentInParent<ObjectInfo>().GetObjectLevel() - 1;
        }
        else
        {
            level = GetComponent<ObjectInfo>().GetObjectLevel() - 1;
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController");

        //repeats the growthCycle Function after x amount of  time, every y amount of time

        InvokeRepeating("GrowthCycle", 2.0f, 10.0f);
    }

    public void reduceMoney(float red)
    {
        //find multiplier => 1(100%) - red * 0.01 (% reduction)
        float reduction = 1.0f - (red * 0.01f);

        for (int i = 0; i < 3; i++)
        {
            moneyOutput[i] = Mathf.RoundToInt((float)moneyOutput[i] * reduction);
        }
    }   
    

    public void reduceFood(float red)
    {
        //find multiplier => 1(100%) - red * 0.01 (% reduction)
        float reduction = 1.0f - (red * 0.01f);

        for (int i = 0; i < 3; i++)
        {
            foodOutput[i] = Mathf.RoundToInt((float)moneyOutput[i] * reduction);
        }
    }

    void GrowthCycle()
    {
        //adds money and food
        gameManager.GetComponent<Currency>().AddMoney(moneyOutput[level]);
        gameManager.GetComponent<FoodScript>().AddFood(foodOutput[level]);        
    }
}
