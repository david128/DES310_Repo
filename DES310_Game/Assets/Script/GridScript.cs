using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //variavles for grid dimensions and layout
    public float xStart, yStart;

    public int columnLength, rowLength;

    public int xSpacing;
    public int zSpacing;


    //initial tile to be placed on grid
    public GameObject gridSquare;

    //list of grid tiles
    List<GameObject> gridSquares = new List<GameObject>();


    public void RemoveGridTile(GameObject ob)//removes tile from list
    {
        gridSquares.Remove(ob);
    }

    public void AddGridTile(GameObject ob)//adds tile to the list
    {
        gridSquares.Add(ob);
    }

   public GameObject GetGridTile(int id)//finds tile from ID
   {
        bool found = false;
        int count = 0;

        while(found == false)
        {
            if (gridSquares[count].GetComponent<ObjectInfo>().GetObjectID() == id)
            {
                
                found = true;
            }

            count += 1;
        }

        if (found)
        {
            return gridSquares[count - 1];
        }
        else
        {
            return null;
        }
        
   }


   public void CreateGrid()
    { 
        int id = 0;

        //minimum spacing of 1
        if(xSpacing< 1)
        {
            xSpacing = 1;
        }

        if (zSpacing < 1)
        {
            zSpacing = 1;
        }

        //create and instansiate each tile, giving a unique ID
        for (int i = 0; i < columnLength; i++)
        {
            for(int j = 0; j< rowLength; j++)
            {
               
                CreateSquare(new Vector3((xSpacing * (i % columnLength)), 1.0f, (zSpacing * (j % rowLength))), id, 0);

                id++;
            }
        }
    }


    void CreateSquare(Vector3 pos, int ID, int type)// create individual tiles, setting ID and type;
    {
        gridSquare.GetComponent<ObjectInfo>().SetObjectID(ID);
        gridSquare.GetComponent<ObjectInfo>().SetObjectType(type);
        gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));

    }
}
