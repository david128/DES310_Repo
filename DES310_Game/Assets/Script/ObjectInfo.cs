﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
  public int ID;
  public int type;

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
}
