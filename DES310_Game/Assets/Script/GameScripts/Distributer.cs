using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    //Public Variables
    public string distributionTitle;

    string distributerChoice;
    public static Distributer instance;

    public string GetDistributor() { return distributerChoice; }

    //bool bigPharma = false, patsys = false, goGreen = false;

    void Start()
    {
        instance = this;

        distributerChoice = "P";   
    }

    // Chooses current Distributor
    public void ChangeDistributor()
    {
        distributerChoice = distributionTitle;
    }
}
