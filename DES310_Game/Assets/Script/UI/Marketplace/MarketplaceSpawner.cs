using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceSpawner : MonoBehaviour
{
    public GameObject menuPrefab;


    public void SpawnMenu()
    {
        GameObject newMenu = Instantiate(menuPrefab,GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;
        LeanTween.scale(GameObject.FindGameObjectWithTag("Marketplace"), new Vector3(1, 1, 1), 0.3f);


    }

}
