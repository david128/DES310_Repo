﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAsset : MonoBehaviour
{
    public GameObject gameManager;

    public void Upgrade(int id)
    {
        //upgrading of asset
        gameManager.GetComponent<Currency>().SetMoney(gameManager.GetComponent<Currency>().GetMoney() - 200); //removes 200 money
        gameManager.GetComponent<AssetChange>().ChangeAsset(id); //upgrade asset
    }
}