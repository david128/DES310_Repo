using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceSpawner : MonoBehaviour
{
    public GameObject menuPrefab;
    public GameObject gameManager;

    public void SpawnMenu()
    {
        gameManager.GetComponent<GameLevelStorer>().GetLevels();
        GameObject newMenu = Instantiate(menuPrefab, GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;
    }
}
