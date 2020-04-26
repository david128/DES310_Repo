using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainabilityScript : MonoBehaviour
{
    public float sustainability;
    float mult =1;

    public float GetSustainability() { return sustainability; }
    public void AddSustainability(float s) { sustainability += s; }
    public void SetSustainability(float s) { sustainability = s; }

    public GameObject gameManager;

    public void SetMultiplier(float m) { mult = m; }

    public void CheckPollution()
    {                      
        sustainability = 0;

        List<GameObject> Grid = gameManager.GetComponent<GridScript>().GetGrid();

        //for each grid square
        for (int i = 0; i < Grid.Count; i++)
        {
            //take out animal stuff, temp cause animals arent in game yet
            if (Grid[i].GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.EMPTY && Grid[i].GetComponent<ObjectInfo>().GetObjectType() !=  ObjectInfo.ObjectType.CHICKEN_COOP && Grid[i].GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.PIG_PEN && Grid[i].GetComponent<ObjectInfo>().GetObjectType() != ObjectInfo.ObjectType.COW_FIELD)
            {
                //if no fill then pol comes from building
                if (Grid[i].GetComponent<ObjectFill>().fillType == ObjectFill.FillType.NONE)
                {
                    if (Grid[i].GetComponent<ObjectInfo>().GetObjectLevel() == 1)
                    {
                        sustainability += Grid[i].GetComponent<ObjectPollution>().pol_lvl1;
                    }
                    else if (Grid[i].GetComponent<ObjectInfo>().GetObjectLevel() == 2)
                    {
                        sustainability += Grid[i].GetComponent<ObjectPollution>().pol_lvl2;
                    }
                    else
                    {
                        sustainability += Grid[i].GetComponent<ObjectPollution>().pol_lvl3;
                    }
                }
                else
                {
                    //else polution comes from fill

                    if (Grid[i].GetComponent<ObjectInfo>().GetObjectLevel() == 1)
                    {
                        sustainability += Grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl1;
                    }
                    else if (Grid[i].GetComponent<ObjectInfo>().GetObjectLevel() == 2)
                    {
                        sustainability += Grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl2;
                    }
                    else
                    {
                        sustainability += Grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl3;
                    }
                }
            }
        }

        sustainability = sustainability * mult;
    }
}
