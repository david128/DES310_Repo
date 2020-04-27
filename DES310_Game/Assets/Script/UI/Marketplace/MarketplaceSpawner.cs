using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceSpawner : MonoBehaviour
{
    public GameObject menuPrefab;

    public void SpawnMenu()
    {
        GameObject newMenu = Instantiate(menuPrefab, GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;

        //Gives marketplace menu a while awake script component
        newMenu.gameObject.AddComponent<WhileAwake>();

        newMenu.transform.SetSiblingIndex(6);
    }
}
