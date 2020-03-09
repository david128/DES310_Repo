using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_WaterSound : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("Water", gameObject);

    }
}
