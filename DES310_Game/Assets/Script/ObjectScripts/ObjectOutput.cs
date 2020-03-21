using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutput : MonoBehaviour
{
    GameObject gameManager;

    public int moneyOutput;
    public int foodOutput;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        InvokeRepeating("GrowthCycle", 15.0f, 1.0f);
    }

    void GrowthCycle()
    {
        gameManager.GetComponent<Currency>().AddMoney(moneyOutput);
        gameManager.GetComponent<FoodScript>().AddFood(foodOutput);        
    }

}
