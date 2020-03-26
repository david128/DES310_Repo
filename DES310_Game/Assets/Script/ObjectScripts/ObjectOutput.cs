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
        InvokeRepeating("GrowthCycle", 15.0f, 1.0f);

        
    }

    void GrowthCycle()
    {
        //adds money and food
        gameManager.GetComponent<Currency>().AddMoney(moneyOutput[level]);
        gameManager.GetComponent<FoodScript>().AddFood(foodOutput[level]);        
    }

}
