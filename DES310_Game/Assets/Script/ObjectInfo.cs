using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
  public int ID; //Unique ID
  public int type; //type
  public int level; //level of upgrade.


    //getters and setters
   public int GetObjectType()
    {
        return type;
    }

    public void SetObjectType(int t)
    {
        type = t;
    }

    public int GetObjectID()
    {
        return ID;
    }

    public void SetObjectID(int id)
    {
        ID = id;
    }

    public int GetObjectLevel()
    {
        return level;
    }

    public void SetObjectLevel(int l)
    {
        level = l;
    }
}
