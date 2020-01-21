using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

    public bool controlType;//true for mobile, false for pc

    //get Inut to be called in main game loop
    void GetInput()
    {
        if (controlType == false)//pc
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                //casts a ray from camera to mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Check what has been clicked/touched//


                //Checks if the ray connects with an object/asset
                if (Physics.Raycast(ray, out hit))
                {
                    int targetType = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType(); //object we are clicking
                    switch(targetType)
                    {
                        case 1:
                            //run selet object type 1 select
                            break;

                        default:
                            //run default click action
                            break;
                    }
                    

                }
                }
                }
            }

        }
    }
