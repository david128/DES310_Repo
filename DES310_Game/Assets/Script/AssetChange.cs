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
        //gets input
        if (controlType ==true)//mobile
        {
            if (Input.touchCount> 0)
            {
                //Declares variables
                RaycastHit hit;

                //casts a ray from camera to mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                //Check what has been clicked/touched//


                //Checks if the ray connects with an object/asset
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType() == 0)
                    {
                        Debug.Log("hit");

                        //Checks if the player has enough money to upgrade the object
                        if (gameObject.GetComponent<Currency>().GetMoney() >= 500)
                        {
                            //Calls change function
                            ChangeAsset();

                            gameObject.GetComponent<Currency>().SetMoney(gameObject.GetComponent<Currency>().GetMoney() - 500);
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Declares variables
                RaycastHit hit;

                //casts a ray from camera to mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Check what has been clicked/touched//


                //Checks if the ray connects with an object/asset
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType() == 0)
                    {
                        Debug.Log("hit");

                        //Checks if the player has enough money to upgrade the object
                        if (gameObject.GetComponent<Currency>().GetMoney() >= 500)
                        {
                            //Calls change function
                            ChangeAsset();

                            gameObject.GetComponent<Currency>().SetMoney(gameObject.GetComponent<Currency>().GetMoney() - 500);
                        }
                    }
                }
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