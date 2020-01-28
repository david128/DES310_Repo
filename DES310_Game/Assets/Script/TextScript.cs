using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text moneyText;
    public Text foodText;
    public GameObject gameManager;

    public void UpdateText()
    {
        foodText.text = ("Food:" + gameManager.GetComponent<FoodScript>().getFood().ToString());
        moneyText.text = ("Money:" + gameManager.GetComponent<Currency>().GetMoney().ToString());
 
    }



}
