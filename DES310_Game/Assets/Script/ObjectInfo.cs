using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    //Declares Object variables
    public int ID;
    public int type;
    public int level;

    //getters and setters for object
    public int GetObjectType() {return type;}

    public void SetObjectType(int t) { type = t; }

    public int GetObjectID() { return ID; }

    public void SetObjectID(int id) { ID = id; }

    public int GetObjectLevel() { return level; }

    public void SetObjectLevel(int lvl) { level = lvl; }

}
