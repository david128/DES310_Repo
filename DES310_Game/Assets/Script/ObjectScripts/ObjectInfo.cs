using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    //Declares Object variables
    public int ID;
    public ObjectType objectType;

    public enum ObjectType
    {
        NONE = 0,
        EMPTY = 1,
        FARMHOUSE = 2,
        FIELD = 3,
        RICE = 4,
        BARN = 5,
        CHICKEN_COOP = 6,
        PIG_PEN = 7,
        COW_FIELD = 8,
        RESEARCH = 9,
        VERTICAL_FARM = 10,
        LOCKED = 11,
        MEAT_LAB = 12
    };
  
    public int level;

    //getters and setters for object
    public ObjectType GetObjectType() {return objectType;}

    public void SetObjectType(ObjectType t) { objectType = t; }

    public int GetObjectID() { return ID; }

    public void SetObjectID(int id) { ID = id; }

    public int GetObjectLevel() { return level; }

    public void SetObjectLevel(int lvl) { level = lvl; }
}
