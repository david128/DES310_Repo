using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    float zoom;

    float startY;

    private void Start()
    {
        zoom = 100.0f;
        startY = 10.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        }

        if (Input.GetKey("s"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        }

        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z );
        }

        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }

        if (Input.GetKey("i") && zoom < 200)
        {
            zoom = zoom + 1;
        }


        if (Input.GetKey("o") && zoom > -100)
        {
            zoom = zoom - 1;
        }

        transform.position = new Vector3 (transform.position.x,(((zoom / 100.0f) * startY)), transform.position.z);


    }
}
