using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update

    public int money; //Static makes this variable a Member of the class and not of any particular instance
    public float time;

    void Start()
    {
        money = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        //Adds delta time between frames to the time variable
        time += Time.deltaTime;

        //When the time gets to 5 seconds the money will increase causing a passive income
        if(time > 3)
        {
            //Increases the money
            money += 100;

            //Resets the period of the passive income
            time = 0.0f;

        }
    }
   public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int mon)
    {
      money = mon;
    }
}
