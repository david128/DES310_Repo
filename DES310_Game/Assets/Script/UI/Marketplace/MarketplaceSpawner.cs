using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceSpawner : MonoBehaviour
{
    //public variable to be set in inspector
    public GameObject menuPrefab;

    //spawns menu
    public void SpawnMenu()
    {
        //instances a new menu and sets it to the marketplace prefab and onto the canvas
        GameObject newMenu = Instantiate(menuPrefab, GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;

        //Gives marketplace menu a while awake script component
        newMenu.gameObject.AddComponent<WhileAwake>();

        //sets the marketplace to the correct position in the canvas 
        //so that the menu is not overlapping anything or behind anything
        newMenu.transform.SetSiblingIndex(6);
    }
}
