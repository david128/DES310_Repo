using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPress: MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("Construction", gameObject);

    }
}