using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Update is called once per frame
    //Inputs for the main camera of the game
   
    public void MoveCamera(Vector2 m)
    {
        if (m.x > 0)
        {
            MoveLeft(Mathf.Abs(m.x) * 0.001f);
        }
        else
        {
            MoveRight(Mathf.Abs(m.x) * 0.001f);
        }

        if (m.y > 0)
        {
            MoveUp(Mathf.Abs(m.y) * 0.003f);
        }
        else
        {
            MoveDown(Mathf.Abs(m.y) * 0.003f);
        }
    }

    public void MoveUp(float v)
    {
        Camera.main.transform.position = new Vector3(transform.position.x + v, transform.position.y, transform.position.z + v);
    }

    public void MoveDown(float v)
    {
        Camera.main.transform.position = new Vector3(transform.position.x - v, transform.position.y, transform.position.z - v);
    }

    public void MoveRight(float v)
    {
        Camera.main.transform.position = new Vector3(transform.position.x - v, transform.position.y, transform.position.z + v);
    }

    public void MoveLeft(float v)
    {
        Camera.main.transform.position = new Vector3(transform.position.x + v, transform.position.y, transform.position.z - v);
    }
}


    

