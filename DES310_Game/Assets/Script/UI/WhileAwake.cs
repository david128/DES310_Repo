using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileAwake : MonoBehaviour
{
    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<InputScript>().GetAllowSelecting() == true && gameObject.activeSelf == true)
        {
            gameManager.GetComponent<InputScript>().SetAllowSelecting(false);
        }
    }
}
