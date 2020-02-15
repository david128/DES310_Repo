using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    float zoom;

    float startY;

    private void Start()
    {
        zoom = 100.0f;
        startY = 10.0f;
        bool fixScritp;
    }


    // Update is called once per frame
    //Inputs for the main camera of the game

    public void MoveCamera(Vector2 m)
    {
        if(m.x > 0)
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
        transform.position = new Vector3(transform.position.x + v, transform.position.y, transform.position.z + v);
    }

    public void MoveDown(float v)
    {
        transform.position = new Vector3(transform.position.x - v, transform.position.y, transform.position.z - v);
    }

    public void MoveRight(float v)
    {
        transform.position = new Vector3(transform.position.x - v, transform.position.y, transform.position.z + v);
    }

    public void MoveLeft(float v)
    {
        transform.position = new Vector3(transform.position.x + v, transform.position.y, transform.position.z - v);
    }



    //if (Input.GetKey("i") && zoom < 200)
    //{
    //    zoom = zoom + 1;
    //}


    //if (Input.GetKey("o") && zoom > -100)
    //{
    //    zoom = zoom - 1;
    //}

    //transform.position = new Vector3 (transform.position.x,(((zoom / 100.0f) * startY)), transform.position.z);
}


    

