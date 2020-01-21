using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{


    public float xStart, yStart;

    public int columnLength, rowLength;

    public int xSpacing;
    public int ySpacing;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void CreateGrid()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            GameObject.Instantiate(Resources.Load("Cube"), new Vector3(xStart + (xSpacing * (i % columnLength),yStart + (ySpacing * (i / columnLength)), Quaternion.identity);
        }
    }

}
