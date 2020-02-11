using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    //Declare food variable
    public float foodOutput;

    //getter and setter
    public void AddFood(float f) { foodOutput += f; }

    public float GetFood() { return foodOutput; }
}
