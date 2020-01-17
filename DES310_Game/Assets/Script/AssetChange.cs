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
    bool whichShape; //true for sphere, false for cube

    void Start()
    {
 
    }

    void ChangeAsset()
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

    //check for input
    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check what has been clicked/touched//

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");

                //Call change function//
                ChangeAsset();

            }
        }
    }

    //everything to be updated
    void Update()
    {
        //get input
        GetInput();
    }

}