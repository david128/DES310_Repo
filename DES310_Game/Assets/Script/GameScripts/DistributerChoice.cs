using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributerChoice : MonoBehaviour
{
    public GameObject gameManager;

    //Choice
    private string distributerChoice;

    //getters
    public string GetDistributerChoice() { return distributerChoice; }

    // Chooses current Distributor
    public void ChangeDistributer()
    {
        distributerChoice = gameManager.GetComponent<DistributerInfo>().GetDistributerTitle();
    }
}
