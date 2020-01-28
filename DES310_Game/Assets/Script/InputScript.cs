using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

    public bool controlType;//true for mobile, false for pc
    public GameObject gameManager;

    //get Inut to be called in main game loop
    public void GetInput()
    {
        if (controlType == false)//pc
        {
            if (Input.GetMouseButtonDown(0))
            {

                select(Input.mousePosition);

            }
            else //mobile
            {
                if (Input.touchCount > 0)
                {

                    select(Input.GetTouch(0).position);
                }
            }


        }
    }

    void select(Vector2 pos)
    {
        RaycastHit hit;

        //casts a ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(pos);

        //Check what has been clicked/touched//


        //Checks if the ray connects with an object/asset
        if (Physics.Raycast(ray, out hit))
        {
            int targetType = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType(); //object we are clicking
            int id = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectID(); //object we are clicking

            switch (targetType)
            {
                case 0:
                    //run selet object type 1 select
                    if (gameManager.GetComponent<Currency>().GetMoney() >= 200)
                    {
                        gameManager.GetComponent<Currency>().SetMoney(gameManager.GetComponent<Currency>().GetMoney() - 200);
                        gameManager.GetComponent<AssetChange>().ChangeAsset(id);
                    }

                    
                    
                    break;

                default:
                    //run default click action
                    break;
            }


        }
    }

}


