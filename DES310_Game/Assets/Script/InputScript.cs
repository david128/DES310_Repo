using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputScript : MonoBehaviour
{
    //Declare variables
    public bool controlType;//true for mobile, false for pc
    public GameObject gameManager;

    public int selectedID;
    public bool selecting = true;

    public void AllowSelecting()
    {
        selecting = true;     

    }

    public int GetSelectedID()
    {
        return selectedID;
    }

    //get Input to be called in main game loop
    public void GetInput()
    {
        //Checks which input type is being used
        if (controlType == false)//pc
        {
            if (Input.GetMouseButtonDown(0))
            {

                Select(Input.mousePosition);

            }
           


        }
        else //mobile
        {
            if (Input.touchCount > 0)
            {

                Select(Input.GetTouch(0).position);
            }
        }
    }
    
    //Finds what object is being selected
    void Select(Vector2 pos)
    {
        //Declare variables
        RaycastHit hit;

        //casts a ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(pos);

        //Checks if the ray connects with an object/asset
        if (Physics.Raycast(ray, out hit))
        {
            //Decalares object variables
            int targetType = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType(); //object we are clicking's type
            int id = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectID(); //object we are clicking's ID

            switch (targetType)
            {
                case 0:
                    //Check that have enough money and that maxLevel of asset has not been reached
                    if (gameManager.GetComponent<Currency>().GetMoney() >= 200 && hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectLevel() != 2 &&selecting == true)
                    {
                        //display button
                        gameManager.GetComponent<ButtonFunctions>().Enable();
                        
                        selectedID = id;
                        selecting = false;
                                                                      


                    }

                    break;

                default:
                    //run default click action
                    break;
            }
        }
    }

    
}


