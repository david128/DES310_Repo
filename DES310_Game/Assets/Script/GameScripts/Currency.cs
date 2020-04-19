using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public GameObject gameManager;

    //Declare money variable
    public int money;

    //getter and setter
    public int GetMoney() { return money; }
    public void SetMoney(int mon) { money = mon; }

    //Add to current money
    public void AddMoney(int m) 
    {
        if (m > 0)
        {
            gameManager.GetComponent<GameLoop>().AddToTotalMoneyEarned(m);
        }
        else
        {
            gameManager.GetComponent<GameLoop>().AddToTotalMoneySpent(m);
        }

        money = money + m; 
    }
}
