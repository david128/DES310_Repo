using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public float xStart, yStart;

    public int columnLength, rowLength;

    public int xSpacing;
    public int zSpacing;

    public GameObject gridSquare;

    List<GameObject> gridSquares = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        if(xSpacing< 1)
        {
            xSpacing = 1;
        }

        if (zSpacing < 1)
        {
            zSpacing = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void CreateGrid()
    { 
        int id = 0;

        for (int i = 0; i < columnLength; i++)
        {
            for(int j = 0; j< rowLength; j++)
            {
                //GameObject.Instantiate(Resources.Load("Cube"), new Vector3((xSpacing * (i % columnLength)), 1.0f, (ySpacing * (i / rowLength))), Quaternion.identity);
                CreateSquare(new Vector3((xSpacing * (i % columnLength)), 1.0f, (zSpacing * (j % rowLength))), id, 0);

                id++;
            }
        }
    }


    void CreateSquare(Vector3 pos, int ID, int type)
    {
        gridSquare.GetComponent<ObjectInfo>().SetObjectID(ID);
        gridSquare.GetComponent<ObjectInfo>().SetObjectType(type);
        gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));

    }
}
