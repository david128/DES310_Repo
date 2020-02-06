using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //Declare variables for grid dimensions and layout
    public float xStart, yStart;
    public int columnLength, rowLength;
    public int xSpacing, zSpacing;

    //initial tile to be placed on grid
    public GameObject gridSquare;

    //list if tiles on grid
    List<GameObject> gridSquares = new List<GameObject>();

    //removes tile from the grid list
    public void RemoveGridTile(GameObject ob) { gridSquares.Remove(ob); }

    //adds tile from the grid list
    public void AddGridTile(GameObject ob) { gridSquares.Add(ob); }

    //Gets grid tile from ID of tile
    public GameObject GetGridTile(int id)
    {
        //Decalre variables
        bool found = false;
        int count = 0;

        //looks throught the list to find the one being used
        while (found == false)
        {
            if (gridSquares[count].GetComponent<ObjectInfo>().GetObjectID() == id)
            {
                found = true;
            }

            count += 1;
        }

        //returns the grid when found otherwise it returns nothing
        if (found)
        {
            return gridSquares[count - 1];
        }
        else
        {
            return null;
        }
   }

    //Creates the grid with the tiles and spacing betwen them
   public void CreateGrid()
    { 
        //Declares variable
        int id = 0;

        //Creates minimum spacing if the default is below 1
        if(xSpacing< 1)
        {
            xSpacing = 1;
        }

        if (zSpacing < 1)
        {
            zSpacing = 1;
        }

        //creates and instantiates each tile, giving them a unique ID
        for (int i = 0; i < columnLength; i++)
        {
            for(int j = 0; j< rowLength; j++)
            {
                //Calls Create Square function to place a unique tile
                CreateSquare(new Vector3((xSpacing * (i % columnLength)), 1.0f, (zSpacing * (j % rowLength))), id);

                id++;
            }
        }
    }

    //creates individual tiles, setting ID and types
    void CreateSquare(Vector3 pos, int ID)
    {
        //Sets default grids components and locations of assets
        if (ID == 1)
        {
            gridSquares.Add((GameObject)Instantiate(Resources.Load("Barn"), pos, Quaternion.identity));
            gridSquares[ID].GetComponent < ObjectInfo>().SetObjectID(ID);
            gridSquares[ID].GetComponent<ObjectInfo>().SetObjectType(ObjectInfo.ObjectType.BARN);
            gridSquares[ID].GetComponent<ObjectInfo>().SetObjectLevel(1);
        }
        else if (ID == 2)
        {

            gridSquares.Add((GameObject)Instantiate(Resources.Load("Farmhouse"), pos, Quaternion.identity));
            gridSquares[ID].GetComponent<ObjectInfo>().SetObjectID(ID);
            gridSquares[ID].GetComponent<ObjectInfo>().SetObjectType(ObjectInfo.ObjectType.FARMHOUSE);
            gridSquares[ID].GetComponent<ObjectInfo>().SetObjectLevel(1);

        }
        else
        {
            gridSquare.GetComponent<ObjectInfo>().SetObjectID(ID);
            gridSquare.GetComponent<ObjectInfo>().SetObjectType(ObjectInfo.ObjectType.EMPTY);
            gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
        }
    }
}
