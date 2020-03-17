using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelStorer
{
    GameObject farmhouse;
    GameObject barn;
    GameObject research;

    public GameObject gameManager;

    int[] levels = new int[3];

    public int[] GetLvls() { return levels; }

    // Start is called before the first frame update
    public void GetLevels()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        //gets grid tiles with default buildings
        farmhouse = gameManager.GetComponent<GridScript>().GetGridTile(23);
        barn = gameManager.GetComponent<GridScript>().GetGridTile(10);
        research = gameManager.GetComponent<GridScript>().GetGridTile(9);

        //Sets level of each default
        levels[0] = farmhouse.GetComponent<ObjectInfo>().GetObjectLevel();
        levels[1] = barn.GetComponent<ObjectInfo>().GetObjectLevel();
        levels[2] = research.GetComponent<ObjectInfo>().GetObjectLevel();
    }
}
