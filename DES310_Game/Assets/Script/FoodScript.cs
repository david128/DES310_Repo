using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{

    public float foodOutput;

    public void addFood(float f)
    {
        foodOutput += f;
    }

    public float getFood()
    {
        return foodOutput;
    }
}
