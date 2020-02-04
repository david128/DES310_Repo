using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    //Declare money variable
    public int money;

    //getter and setter
    public int GetMoney() { return money; }

    public void SetMoney(int mon) { money = mon; }
}
