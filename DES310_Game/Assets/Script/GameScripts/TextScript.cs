using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    //Declares UI variables
    public TextMeshProUGUI moneyText;
    public Text foodText;
    public Text fpsText;
    public GameObject gameManager;

    //Updates text on screen
    public void UpdateText()
    {
        //translates into strings to be displayed
        foodText.text = ("Food:" + gameManager.GetComponent<FoodScript>().GetFood().ToString());
        moneyText.text = (gameManager.GetComponent<Currency>().GetMoney().ToString());
        fpsText.text = ("FPS: " + gameManager.GetComponent<GameLoop>().GetFPS().ToString());
    }
}
