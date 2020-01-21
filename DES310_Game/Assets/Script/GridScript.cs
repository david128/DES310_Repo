using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public float xStart, yStart;

    public int columnLength, rowLength;

    public float xSpacing;
    public float ySpacing;

    // Start is called before the first frame update
    void Start()
    {
        if(xSpacing< 1)
        {
            xSpacing = 1;
        }

        if (ySpacing < 1)
        {
            ySpacing = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void CreateGrid()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            GameObject.Instantiate(Resources.Load("Cube"), new Vector3((xSpacing * (i % columnLength)), 1.0f, (ySpacing * (i / rowLength))), Quaternion.identity);
        }
    }

}
