using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    //Declare food variable
    public int food;

    //getter and setter
    public int GetFood() { return food; }
    public void SetFood(int f) { food = f; }

    //Add to current food
    public void AddFood(int f) { food = food + f; }

}
