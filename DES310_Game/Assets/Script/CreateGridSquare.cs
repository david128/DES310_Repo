using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class CreateGridSquare : MonoBehaviour
{

    public GameObject gridSquare;

    List<GameObject> gridSquares = new List<GameObject>();

    public void create(Vector3 pos, int ID, int type)
    {
        gridSquare.GetComponent<ObjectInfo>().SetObjectID(ID);
        gridSquare.GetComponent<ObjectInfo>().SetObjectType(type);
        gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));

    }

}
