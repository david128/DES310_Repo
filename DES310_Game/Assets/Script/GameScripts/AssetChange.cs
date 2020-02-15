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
    public bool controlType; //true for mobile, false for pc

    public GameObject gameManager;
    GameObject newAsset;


    public void Upgrade(int id) //upgrade level and then change asset
    {
       // int oldID;
        GameObject asset;
        asset = gameManager.GetComponent<GridScript>().GetGridTile(id);

        //get level and upgrade
        int level = asset.GetComponent<ObjectInfo>().GetObjectLevel() + 1;

        //get type
        ObjectInfo.ObjectType type = asset.GetComponent<ObjectInfo>().GetObjectType();

        ChangeAsset(id, level, type);
    }

    public void Demolish(int id) //change to empty
    {

        ChangeAsset(id, 0, ObjectInfo.ObjectType.EMPTY);
    }

    public void Build(int id, ObjectInfo.ObjectType type)//build new tile
    {
        ChangeAsset(id, 1, type);
    }


    public GameObject LoadAsset(ObjectInfo.ObjectType type, Transform transform, int level)//load asset based on type
    {

        string lvlExtension;

        if(level == 2)//finds correct extension for name of resource
        {
            lvlExtension = "_lvl2";
        }
        else if (level == 3)
        {
            lvlExtension = "_lvl3";
        }
        else
        {
            lvlExtension = ""; //no extension needed.
        }

        switch(type)
        {
            case ObjectInfo.ObjectType.EMPTY:
                return (GameObject)Instantiate(Resources.Load("Empty"), transform.position, Quaternion.identity);
                
            case ObjectInfo.ObjectType.BARN:
                return (GameObject)Instantiate(Resources.Load("Barn" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.FARMHOUSE:
                return (GameObject)Instantiate(Resources.Load("Farmhouse" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.FIELD:
                return (GameObject)Instantiate(Resources.Load("Field" + lvlExtension), transform.position, Quaternion.identity);
                
            default:
                return (GameObject)Instantiate(Resources.Load("Field" + lvlExtension), transform.position, Quaternion.identity);
                

        }
          
    }


    public void ChangeAsset(int id, int level, ObjectInfo.ObjectType type)
    {

        //find asset and keep transform
        GameObject asset; 
        asset = gameManager.GetComponent<GridScript>().GetGridTile(id);
        Transform transform = asset.transform;

        //load correct asset based on type
        newAsset = LoadAsset(type, transform, level);

        //remove from list
        gameManager.GetComponent<GridScript>().RemoveGridTile(asset);

        //Destroy shape to be replaced
        GameObject.Destroy(asset);

        //set objectID and level
        newAsset.GetComponent<ObjectInfo>().SetObjectID(id);
        newAsset.GetComponent<ObjectInfo>().SetObjectLevel(level);

        //Add from list
        gameManager.GetComponent<GridScript>().AddGridTile(newAsset);
    }

    //public void ChangeAsset(int id)
    //{
    //    //Declare variables
    //    GameObject asset;
    //    Transform transform;

    //    int oldID;
    //    int level;

    //    //sets asset to the object of what is being selected
    //    asset = gameManager.GetComponent<GridScript>().GetGridTile(id);

    //    //get asset transform
    //    transform = asset.transform;

    //    //gets old assets ID
    //    oldID = asset.GetComponent<ObjectInfo>().GetObjectID();

    //    //get level and upgrade
    //    level = asset.GetComponent<ObjectInfo>().GetObjectLevel() + 1;

    //    //Remove from list
    //    gameManager.GetComponent<GridScript>().RemoveGridTile(asset);

    //    //Destroy shape to be replaced
    //    GameObject.Destroy(asset);

    //    //check what the shape is currently at adn instantiate the other shape
    //    if (level == 1)
    //    {
    //        //GameObject.Instantiate(Resources.Load("Sphere"), transform.position, transform.rotation);
    //        newAsset =(GameObject)Instantiate(Resources.Load("Sphere"), transform.position, Quaternion.identity);

    //        //set objectID and level
    //        newAsset.GetComponent<ObjectInfo>().SetObjectID(oldID);
    //        newAsset.GetComponent<ObjectInfo>().SetObjectLevel(level);
            
    //        //Add from list
    //        gameManager.GetComponent<GridScript>().AddGridTile(newAsset);
    //    }
    //    else
    //    {
    //        //GameObject.Instantiate(Resources.Load("Sphere"), transform.position, transform.rotation);
    //        newAsset = (GameObject)Instantiate(Resources.Load("Cube"), transform.position, Quaternion.identity);

    //        //set objectID and level
    //        newAsset.GetComponent<ObjectInfo>().SetObjectID(oldID);
    //        newAsset.GetComponent<ObjectInfo>().SetObjectLevel(level);

    //        //Add from list
    //        gameManager.GetComponent<GridScript>().AddGridTile(newAsset);
    //    }
    //}

}