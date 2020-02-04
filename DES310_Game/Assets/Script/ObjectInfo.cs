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
        EMPTY = 0,
        FARMHOUSE = 1,
        FIELD = 2,
        BARN = 3
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
