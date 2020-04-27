using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollution : MonoBehaviour
{
    [SerializeField] public float pol_lvl1;
    [SerializeField] public float pol_lvl2;
    [SerializeField] public float pol_lvl3;

    float[] pol;

    public float[] GetPolValues() { pol[0] = pol_lvl1; pol[1] = pol_lvl2; pol[2] = pol_lvl3; return pol;}

    void Start() 
    {
        pol = new float[3];

        pol[0] = pol_lvl1;
        pol[1] = pol_lvl2;
        pol[2] = pol_lvl3;
    }

}
