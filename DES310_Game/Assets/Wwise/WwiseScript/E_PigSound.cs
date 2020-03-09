using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PigSound : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("Pig", gameObject);

    }
}
