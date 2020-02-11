using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //Declare variables
    public GameObject gameManager, textManager;
    public float time;

    public float FPS;

    // Start is called before the first frame update
    void Start()
    {
        //Creates grid at the start
        gameManager.GetComponent<GridScript>().CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        //Adds up time passed every frame
        time += Time.deltaTime;

        FPS = 1.0f / Time.deltaTime;

       //FPS += (Time.unscaledDeltaTime - FPS) * 0.1f;

        //When the time gets to 3 seconds the money will increase causing a passive income
        if (time > 3)
        {
            //Increases the money
            //gameManager.GetComponent<Currency>().AddMoney(gameManager.GetComponent<Currency>().GetMoney() + 100);

            //Resets the period of time for the passive income
            time = 0.0f;
        }

        //Gets input from player
        gameManager.GetComponent<InputScript>().GetInput();

        //Updates the UI text
        textManager.GetComponent<TextScript>().UpdateText();
       

    }

    public float GetFPS() { return FPS; }
}
