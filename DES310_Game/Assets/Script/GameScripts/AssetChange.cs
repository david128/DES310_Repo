﻿/*
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
    GameObject newFill;

    public void Upgrade(int id) //upgrade level and then change asset
    {
       // int oldID;
        GameObject asset;
        asset = gameManager.GetComponent<GridScript>().GetGridTile(id);

        //get level and upgrade
        int level = asset.GetComponent<ObjectInfo>().GetObjectLevel() + 1;

        //get type
        ObjectInfo.ObjectType type = asset.GetComponent<ObjectInfo>().GetObjectType();

        //set fill
        ObjectFill.FillType fill;

        if (type == ObjectInfo.ObjectType.FIELD)
        {
            fill = asset.GetComponent<ObjectFill>().GetFillType();
        }
        else
        {
            fill = ObjectFill.FillType.NONE;
        }

        ChangeAsset(id, level, type, fill);
    }

    public void Demolish(int id) //change to empty
    { 
        ChangeAsset(id, 0, ObjectInfo.ObjectType.EMPTY, 0);
    }

    public void Build(int id, ObjectInfo.ObjectType type, ObjectFill.FillType fill)//build new tile
    {
        ChangeAsset(id, 1, type, fill);
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

        switch (type)
        {
            case ObjectInfo.ObjectType.EMPTY:
                return (GameObject)Instantiate(Resources.Load("Empty"), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.BARN:
                return (GameObject)Instantiate(Resources.Load("Barn" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.FARMHOUSE:
                return (GameObject)Instantiate(Resources.Load("Farmhouse" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.FIELD:
                return (GameObject)Instantiate(Resources.Load("Field" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.RICE:
                return (GameObject)Instantiate(Resources.Load("RiceField"), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                return (GameObject)Instantiate(Resources.Load("ChickenCoop" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.COW_FIELD:
                return (GameObject)Instantiate(Resources.Load("CowField" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.PIG_PEN:
                return (GameObject)Instantiate(Resources.Load("PigField" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.RESEARCH:
                return (GameObject)Instantiate(Resources.Load("ResearchLab" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                return (GameObject)Instantiate(Resources.Load("VerticalFarm" + lvlExtension), transform.position, Quaternion.identity);

            case ObjectInfo.ObjectType.MEAT_LAB:
                return (GameObject)Instantiate(Resources.Load("MeatLab" + lvlExtension), transform.position, Quaternion.identity);

            default:
                return (GameObject)Instantiate(Resources.Load("Field" + lvlExtension), transform.position, Quaternion.identity);
            
        }    
    }

    public void ChangeAsset(int id, int level, ObjectInfo.ObjectType type, ObjectFill.FillType fill)
    {
        //find asset and keep transform
        GameObject asset; 
        asset = gameManager.GetComponent<GridScript>().GetGridTile(id);

        Transform transform = asset.transform;
        Object locked;
      
        //load correct asset based on type
        newAsset = LoadAsset(type, transform, level);

        //Checks if the farmhouse is being upgraded and if so what level the farmhouse is being upgraded to
        if(type == ObjectInfo.ObjectType.FARMHOUSE && level == 2)
        {
            locked = gameManager.GetComponent<GridScript>().lockLvl2;
            Destroy(locked);
        }
        else if(type == ObjectInfo.ObjectType.FARMHOUSE && level == 3)
        {
            locked = gameManager.GetComponent<GridScript>().lockLvl3;
            Destroy(locked);
        }

        //Checks if the object being cahnged is a field or not to set the fill of the grid
        if (type == ObjectInfo.ObjectType.FIELD || type == ObjectInfo.ObjectType.CHICKEN_COOP || type == ObjectInfo.ObjectType.COW_FIELD || type == ObjectInfo.ObjectType.PIG_PEN)
        {
            //load correct fill based on whats chosen
            newFill = LoadFill(fill, transform);
        }
        else
        {
            fill = ObjectFill.FillType.NONE;
        }

        //remove from list
        gameManager.GetComponent<GridScript>().RemoveGridTile(asset);

        //Destroy shape to be replaced
        GameObject.Destroy(asset);

        //set objectID and level and fill
        newAsset.GetComponent<ObjectInfo>().SetObjectID(id);
        newAsset.GetComponent<ObjectInfo>().SetObjectLevel(level);
        newAsset.GetComponent<ObjectFill>().SetFillType(fill);

        //Add from list
        gameManager.GetComponent<GridScript>().AddGridTile(newAsset);

        //sust has changed so update.
        //gameManager.GetComponent<SustainabilityScript>().CheckPollution();
    }


    GameObject LoadFill(ObjectFill.FillType fill, Transform transform)
    {
        switch (fill)
        {
            case ObjectFill.FillType.NONE:
                return (GameObject)Instantiate(Resources.Load("Sphere"), new Vector3(100.0f, 1000.0f, 0.0f), Quaternion.identity, newAsset.transform);

            case ObjectFill.FillType.WHEAT:
                return (GameObject)Instantiate(Resources.Load("Wheat"), transform.position, Quaternion.identity, newAsset.transform);
                
            case ObjectFill.FillType.CORN:
                return (GameObject)Instantiate(Resources.Load("Corn"), transform.position, Quaternion.identity, newAsset.transform);

            case ObjectFill.FillType.CARROT:
                return (GameObject)Instantiate(Resources.Load("Carrots"), transform.position, Quaternion.identity, newAsset.transform);
                
            case ObjectFill.FillType.POTATO:
                return (GameObject)Instantiate(Resources.Load("Cabbages"), transform.position, Quaternion.identity, newAsset.transform);
                
            case ObjectFill.FillType.TURNIP:
                return (GameObject)Instantiate(Resources.Load("Turnips"), transform.position, Quaternion.identity, newAsset.transform);
                
            case ObjectFill.FillType.COW:

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 2.03f, transform.position.y, transform.position.z + 1.83f), new Quaternion(0.0f, 0.999f, 0.0f, 0.041f), newAsset.transform);

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x - 2.4f, transform.position.y, transform.position.z + 2.31f), new Quaternion(0.0f, 0.287f, 0.0f, 0.958f), newAsset.transform);

                return (GameObject)Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 0.37f, transform.position.y, transform.position.z - 0.43f), new Quaternion(0.0f, 0.827f, 0.0f, -0.563f), newAsset.transform);

            case ObjectFill.FillType.PIG:

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 2.03f, transform.position.y, transform.position.z + 1.83f), new Quaternion(0.0f, 0.999f, 0.0f, 0.041f), newAsset.transform);

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x - 2.4f, transform.position.y, transform.position.z + 2.31f), new Quaternion(0.0f, 0.287f, 0.0f, 0.958f), newAsset.transform);

                return (GameObject)Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 0.37f, transform.position.y, transform.position.z - 0.43f), new Quaternion(0.0f, 0.827f, 0.0f, -0.563f), newAsset.transform);


            case ObjectFill.FillType.CHICKEN:

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 2.03f, transform.position.y, transform.position.z + 1.83f), new Quaternion(0.0f, 0.999f, 0.0f, 0.041f), newAsset.transform);

                Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x - 2.4f, transform.position.y, transform.position.z + 2.31f), new Quaternion(0.0f, 0.287f, 0.0f, 0.958f), newAsset.transform);

                return (GameObject)Instantiate(Resources.Load("Animal"), new Vector3(transform.position.x + 0.37f, transform.position.y, transform.position.z - 0.43f), new Quaternion(0.0f, 0.827f, 0.0f, -0.563f), newAsset.transform);


            case ObjectFill.FillType.SUNFLOWER:
                return (GameObject)Instantiate(Resources.Load("Sunflower"), transform.position, Quaternion.identity, newAsset.transform);
               
            case ObjectFill.FillType.SUGARCANE:
                return (GameObject)Instantiate(Resources.Load("Sugarcane_Optimised"), transform.position, Quaternion.identity, newAsset.transform);

            case ObjectFill.FillType.COCCOA:
                return (GameObject)Instantiate(Resources.Load("Cocoa"), transform.position, Quaternion.identity, newAsset.transform);

            default:
                return null;
        }
    }
}