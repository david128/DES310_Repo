using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update


    public int money;

   public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int mon)
    {
      money = mon;
    }
}
