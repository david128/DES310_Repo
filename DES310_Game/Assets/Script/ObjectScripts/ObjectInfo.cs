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
        BARN = 4,
        CHICKEN_COUP = 5,
        PIG_PEN = 6,
        COW_FIELD =7
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
