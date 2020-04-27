using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadialStats : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject badPol;
    public GameObject goodPol;

    GameObject selectedTile;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI nextMoneyText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI nextFoodText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI moneyPlusSymbol;
    public TextMeshProUGUI foodPlusSymbol;

    public int[] money;
    public int moneyCurrent;
    public int nextMoney;

    public int[] food;
    public int foodCurrent;
    public float nextFood;

    int level;

    public float[] sustValues;
    public float sustainability;
    public int upgradePrice;

    public void GetStats()
    {
        selectedTile = gameManager.GetComponent<GameLoop>().GetSelectedTile();

        ObjectData selectedData = gameManager.GetComponent<GameInfo>().GetTypeInfo(selectedTile.GetComponent<ObjectInfo>().GetObjectType(), selectedTile.GetComponent<ObjectFill>().GetFillType()); //get data relating to target

        //Money and food for stats radial menu
        if (selectedTile.TryGetComponent(out ObjectOutput output))
        {
            //in parent object
            money = output.GetComponent<ObjectOutput>().GetTileMoneyOutput();
            food = output.GetComponent<ObjectOutput>().GetTileFoodOutput();
        }
        else if(selectedTile.GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.FARMHOUSE || selectedTile.GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.BARN || selectedTile.GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.RESEARCH)
        {
            //in child object
            money = selectedTile.GetComponentInChildren<ObjectOutput>().GetTileMoneyOutput();
            food = selectedTile.GetComponentInChildren<ObjectOutput>().GetTileFoodOutput();
        }

        //Sustainability for radial stas menu
        if (selectedTile.TryGetComponent(out ObjectPollution pol))
        {
            //in parent
            sustValues = pol.GetPolValues();
        }
        else if (selectedTile.GetComponentInChildren<ObjectPollution>().GetPolValues() != null)
        {
            //in child
            sustValues = selectedTile.GetComponentInChildren<ObjectPollution>().GetPolValues();
        }
        else
        {
            sustainability = 0;
        }

        //Sets values to show in menu depending on level of field
        if (selectedTile.GetComponent<ObjectInfo>().GetObjectLevel() == 1)
        {
            moneyCurrent = money[0];
            nextMoney = money[1] - moneyCurrent;

            foodCurrent = food[0];
            nextFood = food[1] - foodCurrent;

            level = 1;

            sustainability = sustValues[0];

            upgradePrice = selectedData.level2Cost;
        }
        else if (selectedTile.GetComponent<ObjectInfo>().GetObjectLevel() == 2)
        {
            moneyCurrent = money[1];
            nextMoney = money[2] - moneyCurrent;

            foodCurrent = food[1];
            nextFood = food[2] - foodCurrent;

            level = 2;

            sustainability = sustValues[1];

            upgradePrice = selectedData.level3Cost;
        }
        else
        {
            moneyCurrent = money[2];
            foodCurrent = food[2];

            level = 3;

            sustainability = sustValues[2];
        }
       
        //Parses numeric values into string to show money and food
        moneyText.text = moneyCurrent.ToString();
        foodText.text = foodCurrent.ToString();
        levelText.text = level.ToString();

        //Checks if there is another level to show
        if (selectedTile.GetComponent<ObjectInfo>().GetObjectLevel() != 3)
        {
            nextMoneyText.text = nextMoney.ToString();
            nextFoodText.text = nextFood.ToString();
            upgradeText.text = upgradePrice.ToString();
        }
        else
        {
            nextMoneyText.text = "";
            nextFoodText.text = "";
            moneyPlusSymbol.text = "";
            foodPlusSymbol.text = "";
            upgradeText.text = "Completed";
        }

        if(selectedTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FARMHOUSE || selectedTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.BARN || selectedTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.RESEARCH)
        {
            if(moneyCurrent == 0)
            {
                moneyText.text = "No output";
                nextMoneyText.text = "";
                moneyPlusSymbol.text = "";
            }

            if(foodCurrent == 0)
            {
                foodText.text = "No output";
                nextFoodText.text = "";
                foodPlusSymbol.text = "";
            }
        }

        //gets current sustainability value
        float currentSust = gameManager.GetComponent<SustainabilityScript>().GetSustainability();

        //takes overall sustainability and adds the fields sustainability
        float futureSustainability = currentSust + sustainability;

        if (sustainability == 0)
        {
            goodPol.SetActive(true);
            badPol.SetActive(false);

            goodPol.GetComponent<TextMeshProUGUI>().text = "+ + +";
        }
        else if (futureSustainability <= (sustainability * 3))//Checks how much pollution this field is producing
        {
            //Sets what pollution to show
            badPol.SetActive(false);
            goodPol.SetActive(true);

            goodPol.GetComponent<TextMeshProUGUI>().color = new Color(0.1370509f, 0.6132076f, 0.03181738f);

            if (futureSustainability * 0.6 < currentSust)
            {
                goodPol.GetComponent<TextMeshProUGUI>().text = "+ + +";
            }
            else if (futureSustainability * 0.8 < currentSust)
            {
                goodPol.GetComponent<TextMeshProUGUI>().text = "+ +";
            }
            else
            {
                goodPol.GetComponent<TextMeshProUGUI>().text = "+";
            }

        }
        else
        {
            //Sets what pollution to show
            badPol.SetActive(true);
            goodPol.SetActive(false);

            badPol.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.0f, 0.0f);

            if (futureSustainability * 0.6 > currentSust)
            {
                badPol.GetComponent<TextMeshProUGUI>().text = "-";
            }
            else if (futureSustainability * 0.8 > currentSust)
            {
                badPol.GetComponent<TextMeshProUGUI>().text = "- -";
            }
            else
            {
                badPol.GetComponent<TextMeshProUGUI>().text = "- - -";
            }

        }
    }

    private void Update()
    {
        if(gameManager.GetComponent<InputScript>().GetNewRadialMenu() == true)
        {
            GetStats();
            gameManager.GetComponent<InputScript>().SetNewRadialMenu(false);
        }
    }
}
