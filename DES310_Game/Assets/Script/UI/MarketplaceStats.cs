using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarketplaceStats : MonoBehaviour
{
    GameObject gameManager;

    public MarketplaceOptions mO;
    public GameObject tileInfo;

    public GameObject badPol;
    public GameObject goodPol;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI purchaseText;

    public int[] money;
    public int moneyOut;

    public int[] food;
    public int foodOut;

    public float[] sustValues;
    public float sustainability;
    public int purchasePrice;

    public void GetStats()
    {
        ObjectData selectedData = gameManager.GetComponent<GameInfo>().GetTypeInfo(mO.GetOptType(), mO.GetOptFill()); //get data relating to target

    //    //sets values to be parsed into strings
        money = tileInfo.GetComponent<ObjectOutput>().GetTileMoneyOutput();

        food = tileInfo.GetComponent<ObjectOutput>().GetTileFoodOutput();

        purchasePrice = selectedData.purchaseCost;

        //Parses numeric values into string to show money and food and cost
        moneyText.text = money[0].ToString();
        foodText.text = food[0].ToString();
        purchaseText.text = purchasePrice.ToString();

        if (tileInfo.TryGetComponent<ObjectPollution>(out ObjectPollution pol))
        {
            sustainability = pol.pol_lvl1;
        }
        else if (tileInfo.GetComponentInChildren<ObjectPollution>().GetPolValues() != null)
        {
            //in child
            sustValues = tileInfo.GetComponentInChildren<ObjectPollution>().GetPolValues();
        }
       
        //gets current sustainability value
        float currentSust = gameManager.GetComponent<SustainabilityScript>().GetSustainability();

        //takes overall sustainability and adds the fields sustainability
        float futureSustainability = currentSust + sustainability;

        if (futureSustainability <= (sustainability * 3)) //Checks how much pollution this field is producing
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

    void Start()
    {
        //finds game manager
        gameManager = GameObject.FindWithTag("GameController");

        gameManager.GetComponent<InputScript>().SetNewMarketplaceMenu(true);
    }

    void Update()
    {
        if (gameManager.GetComponent<InputScript>().GetNewMarketplaceMenu() == true)
        {
            gameManager.GetComponent<InputScript>().SetNewMarketplaceMenu(false);

            GetStats();
        }
    }
}
