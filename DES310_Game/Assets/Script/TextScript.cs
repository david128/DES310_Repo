using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    //Declares UI variables
    public Text moneyText;
    public Text foodText;
    public GameObject gameManager;

    //Updates text on screen
    public void UpdateText()
    {
        //translates into strings to be displayed
        foodText.text = ("Food:" + gameManager.GetComponent<FoodScript>().GetFood().ToString());
        moneyText.text = ("Money:" + gameManager.GetComponent<Currency>().GetMoney().ToString());
    }
}
