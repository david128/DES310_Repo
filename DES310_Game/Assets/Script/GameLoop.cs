using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    public GameObject gameManager;


    public float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        //When the time gets to 5 seconds the money will increase causing a passive income
        if (time > 3)
        {
            //Increases the money
            gameManager.GetComponent<Currency>().SetMoney(gameManager.GetComponent<Currency>().GetMoney() + 100);

            //Resets the period of the passive income
            time = 0.0f;

        }

        gameManager.GetComponent<InputScript>().GetInput();

       

    }
}
