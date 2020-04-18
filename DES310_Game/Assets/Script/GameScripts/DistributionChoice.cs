﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributionChoice : MonoBehaviour
{
    //Public variables
    public GameObject gameManager;

    public Button BFB;
    public Button PB;
    public Button GGB;

    //Instance
    public static DistributionChoice instance;

    string distributerChoice;

    //getter
    public string GetDistributionChoice() { return distributerChoice; }

    //setters
    public void SetDefDistributionChoice() { distributerChoice = "P"; }
    public void SetDistributionChoice(string d) { distributerChoice = d; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        distributerChoice = "P";
    }

    // Chooses current Distributor
    public void ChangeDistributer()
    {
        if (BFB.interactable == false)
        {
            distributerChoice = "BF";
        }
        else if (PB.interactable == false)
        {
            distributerChoice = "P";
        }
        else if (GGB.interactable == false)
        {
            distributerChoice = "GG";
        }
    }
}
