using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributionChoice : MonoBehaviour
{
    //Public variables
    public GameObject gameManager;
    public GameObject enterButton;

    public Button BFB;
    public Button PB;
    public Button GGB;

    //Instance
    public static DistributionChoice instance;

    string distributerChoice;

    bool distributionOpen;

    //getter
    public string GetDistributionChoice() { return distributerChoice; }
    public bool GetDistributionOpen() { return distributionOpen; }

    //setters
    public void SetDefDistributionChoice() { distributerChoice = "P"; }
    public void SetDistributionChoice(string d) { distributerChoice = d; }
    public void SetDistributionOpen(bool dO) { distributionOpen = dO; }

    //Sets buttons
    public void SetDistribubuterButtons(string distributer)
    {

        if (distributer == "BF")
        {
            BFB.interactable = false;
            PB.interactable = true;
            GGB.interactable = true;
        }
        else if(distributer == "P")
        {
            BFB.interactable = true;
            PB.interactable = false;
            GGB.interactable = true;
        }
        else if(distributer == "GG")
        {
            BFB.interactable = true;
            PB.interactable = true;
            GGB.interactable = false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        distributerChoice = "P";
    }

    void Update()
    {
        if (gameManager.GetComponent<InputScript>().GetAllowSelecting() == false && enterButton.active == true)
        {
            enterButton.SetActive(false);
        }
        else if (gameManager.GetComponent<InputScript>().GetAllowSelecting() == true && enterButton.active == false)
        {
            enterButton.SetActive(true);
        }
       
        //checks if distribution id open and if selecting is true
        if (gameManager.GetComponent<InputScript>().GetAllowSelecting() == true && distributionOpen == true)
        {
            gameManager.GetComponent<InputScript>().SetAllowSelecting(false);
        }
    }

    // Chooses current Distributor
    public void ChangeDistributer()
    {
        
        if (BFB.interactable == false)
        {
            if (TutorialManager.instance != null && TutorialManager.instance.GetTutorial() == true)
            {
                TutorialEvents.instance.SetChosenDistributor(true);
            }

            distributerChoice = "BF";
            gameManager.GetComponent<SustainabilityScript>().SetMultiplier(1.2f);
        }
        else if (PB.interactable == false)
        {
            distributerChoice = "P";
            gameManager.GetComponent<SustainabilityScript>().SetMultiplier(1.0f);
        }
        else if (GGB.interactable == false)
        {
            distributerChoice = "GG";
            gameManager.GetComponent<SustainabilityScript>().SetMultiplier(0.8f);
        }
    }
}
