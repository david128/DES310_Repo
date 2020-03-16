using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainabilityScript : MonoBehaviour
{
    float sustainability;

    public float GetSustainability() { return sustainability; }
    public void AddSustainability(float s) { sustainability += s; }
    public void SetSustainability(float s) { sustainability = s; }

}
