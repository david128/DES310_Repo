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

    void Start()
    {
        
    }

    public void ChangeAsset()
    {
        //Declare variables
        GameObject shape;
        Transform transform;

        //find shape objects
        shape = GameObject.FindGameObjectWithTag("Shape");

        //get shape transform
        transform = shape.transform;

        //Destroy shape to be replaced
        GameObject.Destroy(shape);

        //check what the shape is currently at adn instantiate the other shape
        if (whichShape == false)
        {
            GameObject.Instantiate(Resources.Load("Sphere"), transform.position, transform.rotation);

            whichShape = true;
        }
        else
        {
            GameObject.Instantiate(Resources.Load("Cube"), transform.position, transform.rotation);
            whichShape = false;
        }
    }





}