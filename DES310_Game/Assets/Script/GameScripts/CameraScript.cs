using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    //Inputs for the main camera of the game
    //movement for camera.
            
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


    

