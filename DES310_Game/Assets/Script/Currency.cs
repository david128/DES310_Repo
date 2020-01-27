using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{

    public int money;

    //getters and setters

   public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int mon)
    {
      money = mon;
    }
}
