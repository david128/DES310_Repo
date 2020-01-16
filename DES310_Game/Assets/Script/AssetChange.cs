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


    void start()
    {
        
    }

    //check for input
    void GetInput()
    {
        Debug.Log("input");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
            }
        }
    }

    //everything to be updated
    void update()
    {
        //get input
        GetInput();
    }

}