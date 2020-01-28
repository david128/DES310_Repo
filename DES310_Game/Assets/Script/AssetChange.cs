/*
Asset change function

Lee Gillan, David Ireland
16/01/2020 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetChange : MonoBehaviour
{
    //Declare variables
    bool whichShape; //true for sphere, false for cube
    public bool controlType; //true for mobile, false for pc

    public GameObject gameManager;
    GameObject newAsset;

    void Start()
    {
        
    }

    public void ChangeAsset(int id)
    {
        //Declare variables
        GameObject asset;
        Transform transform;

        int oldID;

        //find shape objects
        //shape = GameObject.FindGameObjectWithTag("Shape");
        asset = gameManager.GetComponent<GridScript>().GetGridTile(id);

        //get asset transform
        transform = asset.transform;

        //gets old assets ID
        oldID = asset.GetComponent<ObjectInfo>().GetObjectID();

        //Remove from list
        gameManager.GetComponent<GridScript>().RemoveGridTile(asset);

        //Destroy shape to be replaced
        GameObject.Destroy(asset);

        //check what the shape is currently at adn instantiate the other shape
        if (whichShape == false)
        {
            //GameObject.Instantiate(Resources.Load("Sphere"), transform.position, transform.rotation);
            newAsset =(GameObject)Instantiate(Resources.Load("Sphere"), transform.position, Quaternion.identity);

            //set objectID
            newAsset.GetComponent<ObjectInfo>().SetObjectID(oldID);
            
            
            //Add from list
            gameManager.GetComponent<GridScript>().AddGridTile(newAsset);

            whichShape = true;
        }
        else
        {
            //GameObject.Instantiate(Resources.Load("Sphere"), transform.position, transform.rotation);
            newAsset = (GameObject)Instantiate(Resources.Load("Cube"), transform.position, Quaternion.identity);

            //set objectID
            newAsset.GetComponent<ObjectInfo>().SetObjectID(oldID);

            //Add from list
            gameManager.GetComponent<GridScript>().AddGridTile(newAsset);
            whichShape = false;
        }
    }

}