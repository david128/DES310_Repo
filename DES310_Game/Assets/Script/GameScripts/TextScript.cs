using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    //Declares UI variables
    public Text moneyText;
    public Text foodText;
    public Text fpsText;
    public GameObject gameManager;

    //Updates text on screen
    public void UpdateText()
    {
        //translates into strings to be displayed
        foodText.text = (gameManager.GetComponent<FoodScript>().GetFood().ToString());
        moneyText.text = (gameManager.GetComponent<Currency>().GetMoney().ToString());
        fpsText.text = ("FPS: " + gameManager.GetComponent<GameLoop>().GetFPS().ToString());
    }
}
